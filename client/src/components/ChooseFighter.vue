<template>
  <v-flex>
    <v-dialog v-model="dialog" scrollable max-width="400px">
      <template v-slot:activator="{ on }">
        <div align="center">
          <v-progress-circular
            :size="externalLoader == true ? 100 : 200"
            :width="10"
            color="red"
            indeterminate
            v-if="isLoading"
          >Loading..</v-progress-circular>
          <div v-if="!isLoading">
            <h3
              v-if="!fighterLibary"
            >{{fighterColor.replace(fighterColor.charAt(0), fighterColor.charAt(0).toUpperCase())}} fighter</h3>
            <h3 v-if="fighterLibary" color="red darken-1">Fighter</h3>
            <img
              class="unkownFighterImageClass"
              v-if="fighter == ''"
              v-bind:class="fighterLibary == true ? 'fighterImageFighterLibrary' : 'fighterImageChooseFighter'"
              id="unkownFighterImage"
              :src="fighterImage"
              v-on="on"
            />
          </div>
          <div v-if="fighter != '' && !isLoading" class="fighterCard item">
            <div class="img-wrap">
              <v-hover v-slot:default="{ hover }">
                <v-img
                  transition="scale-transition"
                  origin="center center"
                  :key="'image_'+fighter.name"
                  class="white--text align-end"
                  v-bind:class="fighterLibary == true ? 'fighterImageFighterLibrary' : 'fighterImageChooseFighter'"
                  :style="hover ? 'opacity:0.65' : 'opacity:1.0 '"
                  id="fighterImage"
                  :src="fighter.image"
                  v-on="on"
                  @click="chosenFighter = ''"
                >
                  <h3 id="fighterName">{{ fighter.name }}</h3>
                  <p id="fighterNickname">{{ fighter.nickname }}</p>
                </v-img>
              </v-hover>
            </div>
          </div>
        </div>
      </template>
      <v-card dark justify="center">
        <v-card-title>Select a fighter</v-card-title>
        <v-divider></v-divider>
        <v-card-text style="height: 100px;">
          <v-autocomplete
            id="autoComplete"
            v-model="select"
            :loading="fightrersAreLoading"
            :items="fighters"
            item-text="name"
            :search-input.sync="chosenFighter"
            cache-items
            :elevation="10"
            hide-no-data
            hide-details
            label="Choose a fighter"
            rounded
            :color="btnColor"
          >
            <template slot="item" slot-scope="data" @click="chooseFighter(data.item.name)">
              <v-list-item-avatar
                :name="'avatarItem'+data.item.name"
                @click="chooseFighter(data.item.name)"
              >
                <img class="selectionFighterImage" :src="data.item.image" />
              </v-list-item-avatar>
              <v-list-item-content @click="chooseFighter(data.item.name)">
                <v-list-item-title>
                  <span id="chooseFighterFighterTitle">{{data.item.name}}</span>
                </v-list-item-title>
              </v-list-item-content>
            </template>
          </v-autocomplete>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="white darken-1" text @click="chooseFighter()">Confirm</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-flex>
</template>

<script>
import axios from "axios";
import { EventBus } from "../plugins/vuetify";

export default {
  name: "choose-fighter",
  components: {},
  data() {
    return {
      fightrersAreLoading: true,
      isLoading: false,
      dialog: false,
      fighters: [],
      loading: false,
      select: "",
      chosenFighter: "",
      fighter: "",
      hover: false,
      fighterImage: "",
      fighterColorNotProb: "",
      apiString: "https://localhost:7225/graphql",
      externalLoader: false
    };
  },
  props: { btnColor: String, fighterColor: String, fighterLibary: Boolean },
  methods: {
    compare: function(a, b) {
      if (a.name < b.name) return -1;
      if (a.name > b.name) return 1;
      return 0;
    },
    getFighters: async function() {
      const res = await axios.post(this.apiString, {
        query: `
                    query{
                        allFighters {
                            name
                            image
                        }
                    } 
               `
      });
      let sorted = res.data.data.allFighters.sort(this.compare);

      for (let fighter in sorted) {
        if (sorted[fighter].image == null) {
          sorted[fighter].image = "https://i.imgur.com/SDXM8dC.png";
        }
      }

      let topFighterNames = [ //todo: let this come through backend rather than hardcoded list, and create algorithm to return current highest ranked/most relevant fighters 
        "jon jones",
        "henry cejudo",
        "khabib nurmagomedov",
        "cody garbrandt		",
        "conor mcgregor",
        "tony ferguson",
        "max holloway",
        "chan sung jung	",
        "jose aldo",
        "joseph benavidez",
        "marlon moraes	",
        "max holloway",
        "jussier formiga",
        "daniel cormier",
        "deiveson figueiredo",
        "brian ortega",
        "israel adesanya",
        "petr yan",
        "frankie edgar",
        "aljamain sterling",
        "brandon moreno",
        "stipe miocic",
        "dan hooker",
        "zabit magomedsharipov",
        "emil meek",
        "jack hermansson"
      ];

      let topFighters = sorted.filter(fighter => {
        return topFighterNames.includes(fighter.name.toLowerCase());
      });

      topFighters = topFighters.sort(this.compare);
      this.fightrersAreLoading = false;
      this.fighters = topFighters.concat(sorted);
    },
    chooseFighter: async function(optionalName) {
      if (optionalName) {
        this.chosenFighter = optionalName;
        this.dialog = false;
      }

      if (
        this.chosenFighter == null ||
        this.chosenFighter == "" ||
        this.chosenFighter == undefined
      ) {
        this.dialog = false;
        return;
      }

      this.$vuetify.goTo(0);
      this.isLoading = true;

      const res = await this.getOneFighter(this.chosenFighter);
      this.fighter = res.data.data.oneFighter;

      if (this.fighter.image == null) {
        this.fighter.image = "https://i.imgur.com/SDXM8dC.png";
      }
      this.$root.$emit("setfighter", [this.$props.fighterColor, this.fighter]);
      this.isLoading = false;
      this.dialog = false;
    },

    chooseFighterExternal: async function(fighterColor, fighterName) {
      this.isLoading = true;
      this.externalLoader = true;

      const res = await this.getOneFighter(fighterName);
      this.fighter = res.data.data.oneFighter;
      if (this.fighter.image == null) {
        this.fighter.image = "https://i.imgur.com/SDXM8dC.png";
      }
      this.$root.$emit("setfighter", [fighterColor, this.fighter]);
      this.isLoading = false;
    },

     //todo: put all graphql calls in one service or plugin
    getOneFighter: async function(fighterName){
      return await axios.post(this.apiString, {
              query: `
                 query oneFighter($name: String!){
                      oneFighter(name: $name) {
                            name
                            nickname
                            record
                            height
                            heightCm
                            weight
                            weightKg
                            weightClass
                            reach
                            reachCm
                            stance
                            birth
                            slpm
                            strikesAccuracy
                            sapm
                            strikesDefence
                            takedownAverage
                            takedownAccuracy
                            takedownDefence
                            submissionAverage
                            record
                            image
                            matchHistory{
                              redName
                              blueName
                              referee
                              time
                              method
                              weightClass
                              round
                              winner
                              eventName
                              redImage
                              blueImage
                              totalRed
                              totalBlue
                              strikesRed
                              strikesBlue
                              tdRed
                              tdBlue
                              passRed
                              passBlue
                              revRed
                              revBlue
                              subRed
                              subBlue
                              clinchRed
                              clinchBlue
                              groundRed
                              groundBlue
                              distanceRed
                              distanceBlue
                              headRed
                              headBlue
                              bodyRed
                              bodyBlue
                              legRed
                              legBlue
                            }
                        }
                } 
              `,
        variables: { name: fighterName }
      });
    }
  },
  mounted: function() {
    if (process.env.NODE_ENV === "development") {
      this.apiString = "https://localhost:7225/graphql";
    } else {
      this.apiString =
        "https://mmai-api.azurewebsites.net/graphql";
    }

    // Get fighter names and images for choose fighter dialog.
    this.getFighters();


    // Set fighter image based on figtercolor prop
    if (this.$props.fighterColor == "red") {
      this.fighterImage = "https://i.imgur.com/076NZWB.png";
    } else {
      this.fighterImage = "https://i.imgur.com/xOj79XZ.png";
    }

    this.$root.$on("setfighterExternal", fighter => {
      this.fighter = fighter[1]; //fighter is an array, with the figherobject in element at index 1
    });

    // Get fighter data and set fighter if fighterarray[0] matches components fighter color.
    EventBus.$on("setExternalFighter", fighterArray => {
      if (this.$props.fighterColor == fighterArray[0]) {
        this.chooseFighterExternal(fighterArray[0], fighterArray[1]);
      }
    });
  },
  watch: {}
};
</script>
<style>
@import "../../public/css/compareFighters.css";
@import "../../public/css/app.css";
@import "../../public/css/chooseFighter.css";
</style>
