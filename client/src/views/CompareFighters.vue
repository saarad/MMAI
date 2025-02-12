<template>
  <v-layout class="background" v-resize="onResize">
    <v-container id="compareFightersContainer">
      <v-alert
        v-if="predictionError"
        type="error"
      >Please select two fighters before predicting the outcome.</v-alert>
      <v-row
        :class="[(this.redFighter == '' && this.blueFighter == '') ? 'noFightersChosenWhitespace' : '']"
      >
        <!-- Fighter picture row -->
        <v-col md="5" sm="6" order="1" align="right">
          <choose-fighter fighter-color="red" btn-color="red" />
        </v-col>

        <v-col md="5" sm="6" order="3" align="left">
          <choose-fighter fighter-color="blue" btn-color="blue" />
        </v-col>

        <v-col
          v-if="this.redFighter == '' || this.blueFighter == ''"
          md="2"
          order="2"
          class="paddingPredictionSectionOnMd predictionBigScreen dividerBetweenFightersInCompareFighters"
          align="center"
          justify="center"
        ></v-col>

        <v-dialog v-model="predictingWithDifferentWeightclasses" max-width="330">
          <v-card>
            <v-card-title class="headline">Different weightclasses</v-card-title>

            <v-card-text>Predicting the outcome of a fight where the athletes have different weightclasses is not recommended as the result may be very uncertain.</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>

              <v-btn
                color="red darken-1"
                text
                @click="predictingWithDifferentWeightclasses = false"
              >Cancel</v-btn>

              <v-btn color="red darken-1" text @click="predictWinner">Predict</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

        <v-col
          v-if="this.redFighter != '' && this.blueFighter != ''"
          md="2"
          order="2"
          class="predictionBigScreen paddingPredictionSectionOnMd dividerBetweenFightersInCompareFighters"
          align="center"
          justify="center"
        >
          <v-col
            class="slideDown"
            align="center"
            v-if="!winnerHasBeenPredicted && !winnerIsCurrentlyBeingPredicted"
          >
            <v-btn
              v-if="!winnerHasBeenPredicted"
              id="predictBtn"
              dark
              @click="checkWeightclassThenPredict()"
            >Predict winner</v-btn>
          </v-col>
          <v-col align="center" v-if="winnerIsCurrentlyBeingPredicted">
            <v-progress-circular :size="150" :width="10" color="red" indeterminate>Predicting..</v-progress-circular>
          </v-col>
          <v-col align="center" justify="center" v-if="winnerHasBeenPredicted"> <!-- Could be a component (used in fightermodal as well) -->
            <h2>{{ predictedWinner }}</h2>
            <p v-if="predictedWinnerProbability != 0">will win with a {{ predictedWinnerProbability }}% probability</p>
            
                  <v-hover class="affiliate" v-slot:default="{ hover }">
                    <v-card
                    :elevation="hover ? 16 : 2"
                      class="py-12 px-4"
                      color="dark lighten-5"
                      flat
                      @click="goToUrl()"
                    >
                      <div class="text-center">
                        <v-img 
                        src="https://cdnroute.bpsgameserver.com/echo-cdn-origin/content/betsafe/sites/4/2017/10/logo_betsafe_white-responsive.png"
                        ></v-img>
                      </div>
                    </v-card>
                  </v-hover>
            
          </v-col>
        </v-col>
        <v-col
          v-if="this.redFighter != '' && this.blueFighter != ''"
          sm="12"
          order="4"
          class="predictionNormalScreen dividerBetweenFightersInCompareFighters"
          align="center"
          justify="center"
        >
          <v-col align="center" v-if="!winnerHasBeenPredicted && !winnerIsCurrentlyBeingPredicted">
            <v-btn
              v-if="!winnerHasBeenPredicted"
              id="predictBtn"
              dark
              @click="checkWeightclassThenPredict()"
            >Predict winner</v-btn>
          </v-col>

          <v-col align="center" v-if="winnerIsCurrentlyBeingPredicted">
            <v-progress-circular :size="150" :width="10" color="red" indeterminate>Predicting..</v-progress-circular>
          </v-col>
          <v-col align="center" v-if="winnerHasBeenPredicted">
            <h2>{{ predictedWinner }}</h2>
            <p>will win with a {{ predictedWinnerProbability }}% probability</p>
            
                  <v-hover class="affiliate" v-slot:default="{ hover }">
                    <v-card
                    :elevation="hover ? 16 : 2"
                      class="py-12 px-4"
                      color="dark lighten-5"
                      flat
                      @click="goToUrl()"
                    >
                      <div class="text-center">
                        <v-img 
                        src="https://cdnroute.bpsgameserver.com/echo-cdn-origin/content/betsafe/sites/4/2017/10/logo_betsafe_white-responsive.png"
                        ></v-img>
                      </div>
                    </v-card>
                  </v-hover>
            
          </v-col>
        </v-col>
      </v-row>

      <v-row>
        <comparison-table />
      </v-row>

      <!-- Accuracy chart  -->
      <v-row>
        <v-col />
        <v-col align="right" class="mb-4" v-if="this.redFighter != ''">
          <v-card class="cardColor accuracyCard" elevation="24" hover>
            <v-card-title class="headline justify-center">Striking Accuracy</v-card-title>
            <donut-chart
              fighterColor="red"
              title
              :size="isMobile ? '110' : '200'"
              :width="isMobile ? '12' : '25'"
              :value="this.redFighter.strikesAccuracy"
              text="%"
            />
          </v-card>
        </v-col>
        <v-col class="hidden-xs-only" />
        <v-col class="hidden-xs-only" />
        <v-col class="hidden-xs-only" />
        <v-col class="hidden-xs-only" />
        <v-col class="mb-4" align="left" v-if="this.blueFighter != ''">
          <v-card class="cardColor accuracyCard" elevation="24">
            <v-card-title class="headline justify-center">Striking Accuracy</v-card-title>
            <donut-chart
              fighterColor="blue"
              title
              :size="isMobile ? '110' : '200'"
              :width="isMobile ? '12' : '25'"
              :value="this.blueFighter.strikesAccuracy"
              text="%"
            />
          </v-card>
        </v-col>
        <v-col />
      </v-row>

      <!-- Bar chart  -->
      <v-row v-if="(this.redFighter != '' || this.blueFighter != '')">
        <v-col align="center">
          <GChart type="ColumnChart" :data="sigStrikeChartData" :options="sigStrikeChartOptions" />
        </v-col>
      </v-row>

      <v-row v-if="(this.redFighter != '' || this.blueFighter != '')">
        <v-col align="center">
          <GChart type="ColumnChart" :data="defenceChartData" :options="defenceChartOptions" />
        </v-col>
      </v-row>
    </v-container>
  </v-layout>
</template>

<script >
import Vue from "vue";
import ChooseFighter from "../components/ChooseFighter.vue";
import ComparisonTable from "../components/ComparisonTable.vue";
import DonutChart from "../components/DonutChart.vue";
import axios from "axios";
import { GChart } from "vue-google-charts";
import { EventBus } from "../plugins/vuetify";

export default {
  name: "compareFighters",
  components: {
    ChooseFighter,
    GChart,
    ComparisonTable,
    DonutChart
  },
  data() {
    return {
      predictingWithDifferentWeightclasses: false,
      apiString: "https://localhost:7225/graphql",
      isMobile: false,
      redFighterName: "",
      compareFighters: this.$store.state.compareFighters,
      compareFightersHasBeenCalled: false,
      redFighter: "",
      blueFighter: "",
      redFighterIsChosen: this.$store.state.chosenblueFighter != "",
      predictionError: false,
      winnerIsCurrentlyBeingPredicted: false,
      winnerHasBeenPredicted: false,
      predictedWinner: "",
      predictedWinnerProbability: "",
      predictionInProgressValue: 0,
      predictionInProgressInterval: {},
      type: "pie2d",
      width: "80%",
      height: "300",
      dataFormat: "json",

      sigStrikeChartData: [
        [
          "Athlete",
          "Sig. strikes landed per min",
          { role: "style" },
          { role: "annotation" },
          "Sig. strikes absorbed per min",
          { role: "style" },
          { role: "annotation" }
        ]
      ],
      sigStrikeChartOptions: {
        annotations: {
          highContrast: false,
          textStyle: {
            color: "white"
          }
        },
        legend: "none",
        titleTextStyle: { color: "white" },
        backgroundColor: "transparent",
        title: "Significant strikes per minute",
        vAxis: {
          textStyle: { color: "white" },
          gridlines: { color: "#5c5b5a", minSpacing: 60 },
          minorGridlines: { color: "#5c5b5a" }
        },
        hAxis: {
          baselineColor: { color: "black" },
          textStyle: { color: "transparent" },
          minorGridlines: { color: "blue" }
        }
      },
      defenceChartData: [
        [
          "Athlete",
          "Takedown defence",
          { role: "style" },
          { role: "annotation" },
          "Striking defence",
          { role: "style" },
          { role: "annotation" }
        ]
      ],
      defenceChartOptions: {
        annotations: {
          highContrast: false,
          textStyle: {
            color: "white"
          }
        },
        legend: "none",
        titleTextStyle: { color: "white" },
        backgroundColor: "transparent",
        title: "Defence in %",
        vAxis: {
          textStyle: { color: "white" },
          gridlines: { color: "#5c5b5a", minSpacing: 60 },
          minorGridlines: { color: "#5c5b5a" }
        },
        hAxis: {
          baselineColor: { color: "black" },
          textStyle: { color: "transparent" },
          minorGridlines: { color: "blue" }
        }
      }
    };
  },

  methods: {
    secondsSinceDate: function(date) {
      var dif = Date.now() - date;
      return Math.abs(dif);
    },
    goToUrl() {
      window.location =
        "http://record.betsafe.com/_IeFgwgHetDVOhkZVvtkCVGNd7ZgqdRLk/1/";
    },
    onResize: function() {
      if (window.innerWidth < 769) {
        this.isMobile = true;
      } else {
        this.isMobile = false;
      }
    },
    checkWeightclassThenPredict: function() {
      if (this.redFighter.weightClass != this.blueFighter.weightClass) {
        this.predictingWithDifferentWeightclasses = true;
      } else {
        this.predictWinner();
      }
    },
    predictWinner: async function() {
      let dateBeforePrediction = Date.now();

      this.predictingWithDifferentWeightclasses = false;
      if (this.redFighter != "" && this.blueFighter != "") {
        this.winnerIsCurrentlyBeingPredicted = true;
        const res = await axios.post(this.apiString, { //todo: put all axios calls in one service or plugin
          query: `
                query predictedWinner($redName: String!, $blueName: String!) {
                  predictedWinner(redName: $redName, blueName: $blueName) {
                    name
                    prob
                  }
                }
            `,
          variables: {
            redName: this.redFighter.name,
            blueName: this.blueFighter.name
          }
        });

        let predictionTime = this.secondsSinceDate(dateBeforePrediction);
        let minimumPredictionTime = 3000;
        if (predictionTime < minimumPredictionTime) {
          setTimeout(() => {
            this.winnerHasBeenPredicted = true;
            this.winnerIsCurrentlyBeingPredicted = false;
            this.predictedWinner = res.data.data.predictedWinner.name;
            this.predictedWinnerProbability = res.data.data.predictedWinner.prob;
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
      window.scrollTo({
        top: 0,
        left: 0,
        behavior: "smooth"
      });
    },

    goTo: function(url) {
      window.location.href = url;
    },

    updateChartData: function(chartType, row) {
      if (chartType == "accuracy") {
        // Insert row, deletes current red/blue row if present
        if (row[0].includes("Red")) {
          this.sigStrikeChartData = this.sigStrikeChartData.filter(e => {
            return e[0].includes("Blue") || e[0].includes("Athlete");
          });
          this.sigStrikeChartData.splice(1, 0, row);
        } else {
          this.sigStrikeChartData = this.sigStrikeChartData.filter(e => {
            return e[0].includes("Red") || e[0].includes("Athlete");
          });
          this.sigStrikeChartData.push(row);
        }
      } else {
        if (row[0].includes("Red")) {
          this.defenceChartData = this.defenceChartData.filter(e => {
            return e[0].includes("Blue") || e[0].includes("Athlete");
          });
          this.defenceChartData.splice(1, 0, row);
        } else {
          this.defenceChartData = this.defenceChartData.filter(e => {
            return e[0].includes("Red") || e[0].includes("Athlete");
          });
          this.defenceChartData.push(row);
        }
      }
    },
    setfighter: function(fighter) {
      if (fighter[0] == "red") {
        this.redFighter = fighter[1];

        if (this.compareFighters != "") {
          this.compareFighters[0] = fighter[1].name;
        }
        // console.log("redFighter is now:", fighter[1])
        if (this.redFighter.image == null) {
          this.redFighter.image = "https://i.imgur.com/SDXM8dC.png";
        }

        this.redAccuracyPieChartData = [
          ["Accuracy", "Percentage"],
          ["Accuracy", parseInt(this.redFighter.strikesAccuracy)],
          ["Not Accuracy", 100 - this.redFighter.strikesAccuracy]
        ];
        this.updateChartData("accuracy", [
          "Red fighter",
          parseFloat(this.redFighter.slpm),
          "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
          "Landed",
          parseFloat(this.redFighter.sapm),
          "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
          "Absorbed"
        ]);
        this.updateChartData("defence", [
          "Red fighter",
          parseFloat(this.redFighter.takedownDefence),
          "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
          "Takedown",
          parseFloat(this.redFighter.strikesDefence),
          "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
          "Striking"
        ]);
      } else {
        this.blueFighter = fighter[1];
        if (this.compareFighters != "") {
          this.compareFighters[1] = fighter[1].name;
        }

        if (this.blueFighter.image == null) {
          this.blueFighter.image = "https://i.imgur.com/SDXM8dC.png";
        }
        this.blueAccuracyPieChartData = [
          ["Accuracy", "Percentage"],
          ["Accuracy", parseInt(this.blueFighter.strikesAccuracy)],
          ["Not Accuracy", 100 - this.blueFighter.strikesAccuracy]
        ];
        this.updateChartData("accuracy", [
          "Blue fighter",
          parseFloat(this.blueFighter.slpm),
          "stroke-color: #196bb3; stroke-opacity: 0.7; stroke-width: 2; fill-color: #196bb3; fill-opacity: 0.5",
          "Landed",
          parseFloat(this.blueFighter.sapm),
          "stroke-color: #196bb3; stroke-opacity: 0.7; stroke-width: 2; fill-color: #196bb3; fill-opacity: 0.5",
          "Absorbed"
        ]);
        this.updateChartData("defence", [
          "Blue fighter",
          parseFloat(this.blueFighter.takedownDefence),
          "stroke-color: #196bb3; stroke-opacity: 0.7; stroke-width: 2; fill-color: #196bb3; fill-opacity: 0.5",
          "Takedown",
          parseFloat(this.blueFighter.strikesDefence),
          "stroke-color: #196bb3; stroke-opacity: 0.7; stroke-width: 2; fill-color: #196bb3; fill-opacity: 0.5",
          "Striking"
        ]);
      }
      this.winnerHasBeenPredicted = false;
    },
    getFighters: function(fighterArray) {
      EventBus.$emit("setExternalFighter", ["red", this.compareFighters[0]]);
      EventBus.$emit("setExternalFighter", ["blue", this.compareFighters[1]]);
    }
  },
  mounted() {
    window.scrollTo({
      top: 0,
      left: 0,
      behavior: "smooth"
    });
    //let isProd = true;
    if (process.env.NODE_ENV === "development") {
      this.apiString = "https://localhost:7225/graphql";
    } else {
      this.apiString =
        "https://mmai-api.azurewebsites.net/graphql";
    }
    this.$root.$on("setfighter", fighter => {
      this.setfighter(fighter);
    });

    if (this.compareFighters != "") {
      this.getFighters();
    }
  },
  beforeDestroy() {
    EventBus.$off("setExternalFighter");
  }
};
</script>

<style>
@import "../../public/css/compareFighters.css";
@import "../../public/css/app.css";
@import "../../public/css/charts.css";
</style>
