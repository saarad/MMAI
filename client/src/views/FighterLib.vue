<template>
  <v-layout class="background">
    <v-container>
      <v-row :class="[(this.fighter == '') ? 'noFightersChosenWhitespace' : '']">
        <!-- Fighter picture row -->
        <v-col align="center" class="fighterLibFighterCard">
          <choose-fighter v-bind:fighterLibary="true" fighter-color="red" btn-color="red" />
        </v-col>
      </v-row>

      <v-row>
        <single-fighter-data-table />
      </v-row>

      <!-- Accuracy chart  -->
      <v-row transition="fade-transition" :key="this.fighter.name+'Accuracy'">
        <v-col align="center" v-if="this.fighter != ''">
          <donut-chart
            fighterColor="red"
            title="Striking Accuracy"
            size="200"
            width="25"
            :value="this.fighter.strikesAccuracy"
            text="% accuracy"
          />
        </v-col>
      </v-row>

      <!-- Bar chart  -->
      <v-row v-if="this.fighter != ''">
        <v-col align="center">
          <GChart type="ColumnChart" :data="sigStrikeChartData" :options="sigStrikeChartOptions" />
        </v-col>
      </v-row>
      <v-row v-if="this.fighter != ''">
        <v-col align="center">
          <GChart type="ColumnChart" :data="defenceChartData" :options="defenceChartOptions" />
        </v-col>
      </v-row>

      <v-row v-if="this.fighter != ''">
        <match-history :matchHistory="this.fighter.matchHistory" />
      </v-row>
    </v-container>
  </v-layout>
</template>

<script>
import Vue from "vue";
import ChooseFighter from "../components/ChooseFighter.vue";
import SingleFighterDataTable from "../components/SingleFighterDataTable.vue";
import MatchHistory from "../components/MatchHistory/MatchHistory.vue";
import DonutChart from "../components/DonutChart.vue";
import { GChart } from "vue-google-charts";

export default {
  name: "fighterLib",
  components: {
    ChooseFighter,
    GChart,
    SingleFighterDataTable,
    DonutChart,
    MatchHistory
  },
  data() {
    return {
      matchHistory: [],
      fighter: "",
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
      this.fighter = fighter[1];

      if (this.fighter.image == null) {
        this.fighter.image = "https://i.imgur.com/SDXM8dC.png";
      }

      this.redAccuracyPieChartData = [
        ["Accuracy", "Percentage"],
        ["Accuracy", parseInt(this.fighter.strikesAccuracy)],
        ["Not Accuracy", 100 - this.fighter.strikesAccuracy]
      ];
      this.updateChartData("accuracy", [
        "Fighter",
        parseFloat(this.fighter.slpm),
        "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
        "Landed",
        parseFloat(this.fighter.sapm),
        "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
        "Absorbed"
      ]);
      this.updateChartData("defence", [
        "Fighter",
        parseFloat(this.fighter.takedownDefence),
        "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
        "Takedown",
        parseFloat(this.fighter.strikesDefence),
        "stroke-color: #cd1616; stroke-opacity: 0.7; stroke-width: 2; fill-color: #cd1616; fill-opacity: 0.5",
        "Striking"
      ]);
    }
  },
  mounted() {
    window.scrollTo({
      top: 0,
      left: 0,
      behavior: "smooth"
    });

    this.$root.$on("setfighter", fighter => {
      this.setfighter(fighter);
    });
    this.$root.$on("setfighterExternal", fighter => {
      this.setfighter(fighter);
    });
  }
};
</script>
<style>
@import "../../public/css/compareFighters.css";
@import "../../public/css/app.css";
@import "../../public/css/charts.css";
@import "../../public/css/fighterLib.css";
</style>
