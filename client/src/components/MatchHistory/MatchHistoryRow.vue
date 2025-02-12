<template>
  <v-container class="matchHistoryRow">
    <v-row class="topMatchHistoryRow">
      <v-col xs="3" align="right" class="hidden-xs-only">
        <h3>{{match.redName}}</h3>
      </v-col>
      <v-col xs="3" align="center">
        <v-img @click="clickFighter(match.redName)">
          <img class="matchHistoryFighterImage matchHistoryFighterImageRed" :src="match.redImage" />
          <div>
            <span v-if="match.winner=='Red'" class="centeredTextOnFighterImage">WIN</span>
          </div>
        </v-img>
      </v-col>
      
      <v-col xs="3" align="center">
        <v-img v-on:click="clickFighter(match.blueName)">
          <img
            class="matchHistoryFighterImage matchHistoryFighterImageBlue"
            :src="match.blueImage"
          />
          <div>
            <span v-if="match.winner=='Blue'" class="centeredTextOnFighterImage">WIN</span>
          </div>
        </v-img>
      </v-col>
      <v-col xs="3" align="left" class="hidden-xs-only">
        <h3>{{match.blueName}}</h3>
      </v-col>
    </v-row>
    <v-row class="hidden-sm-and-up">
      <v-col xs="2" align="center">{{match.redName}}</v-col>
      <v-col xs="2" align="center">{{match.blueName}}</v-col>
    </v-row>
    <v-row class="bottomMatchHistoryRow">
      <v-col align="center">
        {{match.eventName}}
        <span v-if="match.date" class="matchHistoryRowGrayText">- {{match.date}}</span>
      </v-col>
      <v-col align="center">
        <span class="matchHistoryRowGrayText">Rounds:</span>
        <span>{{match.round}}</span>
      </v-col>
      <v-col align="center">
        <span class="matchHistoryRowGrayText">Method:</span>
        <span>{{match.method}}</span>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from "axios";
import { EventBus } from "../../plugins/vuetify";

export default {
  name: "match-history-row",
  props: {
    match: Object
  },
  components: {},
  data() {
    return {
      apiString: ""
    };
  },
  methods: {
    clickFighter: async function(fighterName) {
      if(this.$router.currentRoute.name != 'fighterLib')
      {
        this.$router.push('/fighterLib')
      }

      const res = await this.getOneFighter(fighterName);
      let fighter = res.data.data.oneFighter;
      this.$root.$emit("setfighterExternal", ["red", fighter]);
      window.scrollTo({
        top: 0,
        left: 0,
        behavior: "smooth"
      });
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
  mounted() {
    if (process.env.NODE_ENV === "development") {
      this.apiString = "https://localhost:7225/graphql";
    } else {
      this.apiString =
        "https://mmai-api.azurewebsites.net/graphql";
    }
  }
};
</script>
<style>
@import "../../../public/css/app.css";
@import "../../../public/css/matchHistory.css";
</style>
