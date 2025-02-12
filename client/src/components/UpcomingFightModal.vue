<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" width="40%" :fullscreen="$vuetify.breakpoint.mdAndDown">
      <v-card dark>
        <v-col align="right">
          <v-icon light @click="setModalFalse" dark>mdi-close</v-icon>
        </v-col>
        <v-card-title>
          <v-row align="center">
            <v-col align="center">
              <v-progress-circular
                :size="200"
                :width="10"
                color="red"
                indeterminate
                v-if="!progress"
              >Loading..</v-progress-circular>
              <span
                v-if="progress"
                class="headline"
              >{{this.redFighter.name}} - {{this.blueFighter.name}}</span>
            </v-col>
          </v-row>
        </v-card-title>
        <v-card-text v-if="progress">
          <v-row>
            <v-col class="mb-4" align="left">
              <v-list-item-avatar size="100">
                <img class="selectionFighterImage" :src="this.redFighter.image" />
              </v-list-item-avatar>
            </v-col>
            <v-col class="mb-4" align="right">
              <v-list-item-avatar size="100">
                <img class="selectionFighterImage" :src="this.blueFighter.image" />
              </v-list-item-avatar>
            </v-col>
          </v-row>
          <v-col
            v-if="this.redFighter != '' && this.blueFighter != ''"
            sm="12"
            order="4"
            align="center"
            justify="center"
          >
            <v-col align="center" v-if="winnerIsCurrentlyBeingPredicted">
              <v-progress-circular :size="120" :width="4" color="red" indeterminate>Predicting..</v-progress-circular>
            </v-col>
            <v-col align="center" v-if="winnerHasBeenPredicted">
              <h2>{{ predictedWinner }}</h2>
              <p v-if="predictedWinnerProbability != 0">will win with a {{ predictedWinnerProbability }}% probability</p>
            </v-col>
          </v-col>
          <v-row>
            <v-col class="mb-4" v-if="this.redFighter == ''"></v-col>
            <v-col class="mb-4" align="right" v-if="this.redFighter != ''">
              <p>{{this.redFighter.birth}}</p>
              <p>{{this.redFighter.stance }}</p>
              <p>{{this.redFighter.weightclass}}</p>
              <p>{{this.redFighter.record}}</p>
            </v-col>
            <v-col class="mb-4" align="center" id="compareFightersStatsMiddleColumn">
              <p>Age</p>
              <p>Stance</p>
              <p>Weightclass</p>
              <p>
                Record
                <v-tooltip right>
                  <template v-slot:activator="{ on }">
                    <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
                  </template>
                  <span>Number of wins-losses-draws</span>
                </v-tooltip>
              </p>
            </v-col>
            <v-col class="mb-4" v-if="this.blueFighter == ''"></v-col>
            <v-col class="mb-4" align="left" v-if="this.blueFighter != ''">
              <p>{{this.blueFighter.birth}}</p>
              <p>{{this.blueFighter.stance }}</p>
              <p>{{this.blueFighter.weightclass}}</p>
              <p>{{this.blueFighter.record}}</p>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="mb-4" v-if="this.redFighter == ''"></v-col>
            <v-col class="mb-4" align="right" v-if="this.redFighter != ''">
              <p>{{this.redFighter.height }}/{{this.blueFighter.heightcm}}cm</p>
              <p>{{this.redFighter.weight }}/{{this.blueFighter.weightkg}}kg</p>
              <p>{{this.redFighter.reach }}/{{this.redFighter.reachcm}}cm</p>
              <p>{{this.redFighter.submissionaverage}}</p>
              <p>{{this.redFighter.takedownaverage}}</p>
            </v-col>
            <v-col class="mb-4" align="center" id="compareFightersStatsMiddleColumn">
              <p>Height</p>
              <p>Weight</p>
              <p>Reach</p>
              <p>
                ASAPM
                <v-tooltip top>
                  <template v-slot:activator="{ on }">
                    <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
                  </template>
                  <span>Average submisssion attempts per match</span>
                </v-tooltip>
              </p>
              <p>
                TA
                <v-tooltip top>
                  <template v-slot:activator="{ on }">
                    <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
                  </template>
                  <span>
                    Takedown Accuracy:
                    The number of takedowns landed divided by the number of
                    attempted takedowns
                  </span>
                </v-tooltip>
              </p>
              <v-btn light small class="hidden-md-and-up" @click="setModalFalse">Close</v-btn>
              <v-btn light medium class="hidden-sm-and-down" @click="setModalFalse">Close</v-btn>
            </v-col>
            <v-col class="mb-4" v-if="this.blueFighter == ''"></v-col>
            <v-col class="mb-4" align="left" v-if="this.blueFighter != ''">
              <p>{{this.blueFighter.height }}/{{this.blueFighter.heightcm}}cm</p>
              <p>{{this.blueFighter.weight }}/{{this.blueFighter.weightkg}}kg</p>
              <p>{{this.blueFighter.reach }}/{{this.blueFighter.reachcm}}cm</p>
              <p>{{this.blueFighter.submissionaverage }}</p>
              <p>{{this.blueFighter.takedownaverage }}</p>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
// @ is an alias to /src

import axios from "axios";
import { router } from "../router/index";
export default {
  name: "upcoming-fight-modal",
  props: {},
  data() {
    return {
      winnerIsCurrentlyBeingPredicted: false,
      winnerHasBeenPredicted: false,
      dialog: this.$store.state.showModal,
      redFighterStore: this.$store.state.chosenRedFighter,
      blueFighterStore: this.$store.state.chosenBlueFighter,
      redFighter: [],
      blueFighter: [],
      progress: false,
      apiString: "https://localhost:7225/graphql"
    };
  },
  watch: {
    dialog(val) {
      !val && this.$store.commit("closeModal");
    }
  },

  methods: {
    secondsSinceDate: function(date) {
      var dif = Date.now() - date;
      return Math.abs(dif);
    },

    predictWinner: async function() {
      let dateBeforePrediction = Date.now();
      this.predictingWithDifferentWeightclasses = false;
      if (this.redFighterName != "" && this.blueFighterName != "") {
        this.winnerIsCurrentlyBeingPredicted = true;
        const res = await axios.post(this.apiString, {
          query: `
                query predictedWinner($redName: String!, $blueName: String!){
                  predictedWinner(redName: $redName, blueName: $blueName) {
                    name
                    prob
                  }
                }
            `,
          variables: {
            redName: this.$store.state.compareFighters[0],
            blueName: this.$store.state.compareFighters[1]
          }
        });
        let predictionTime = this.secondsSinceDate(dateBeforePrediction);
        let minimumPredictionTime = 3000;
        if (predictionTime < minimumPredictionTime) {
          setTimeout(() => {
            this.winnerHasBeenPredicted = true;
            this.winnerIsCurrentlyBeingPredicted = false;
            this.predictedWinner = res.data.data.predictedWinner.name;
            this.predictedWinnerProbability =
              res.data.data.predictedWinner.prob;
            this.predictionError = false;
          }, minimumPredictionTime - predictionTime);
        } else {
          this.winnerHasBeenPredicted = true;
          this.winnerIsCurrentlyBeingPredicted = false;
          this.predictedWinner = res.data.data.predictedWinner.name;
          this.predictedWinnerProbability =
            res.data.data.predictedWinner.prob;
          this.predictionError = false;
        }
      } else {
        this.predictionError = true;
      }

      //Removed scroll to top on predict, comment back in if needed.
      // window.scrollTo({
      //   top: 0,
      //   left: 0,
      //   behavior: "smooth"
      // });
    },

    setModalFalse: function() {
      this.$store.commit("closeModal");
    },

    //todo: Add error handling
    upcomingEventsGetFighterInfo: async function(e) {
      const res = await this.getOneFighter(e[0]);

      this.redFighter = res.data.data.oneFighter;
      if (this.redFighter.image == null) {
        this.redFighter.image = "https://i.imgur.com/SDXM8dC.png";
      }

      const res2 = await this.getOneFighter(e[1]);

      this.blueFighter = res2.data.data.oneFighter;

      if (this.blueFighter.image == null) {
        this.blueFighter.image = "https://i.imgur.com/SDXM8dC.png";
      }
      this.progress = true;
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
    this.upcomingEventsGetFighterInfo(this.$store.state.compareFighters);
    this.predictWinner();
  }
};
</script>
<style>
</style>

