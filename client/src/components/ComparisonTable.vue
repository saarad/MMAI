<template>
  <v-container v-if="this.blueFighter != '' || this.redFighter != ''">
    <v-divider class="dividerWithPadding" />
    <v-row>
      <v-col mb="4" v-if="this.redFighter == ''"></v-col>
      <v-col mb="4" align="right" v-if="this.redFighter != ''">
        <p>{{this.redFighter.birth}}</p>
        <p>{{this.redFighter.stance }}</p>
        <p>{{this.redFighter.weightClass}}</p>
        <p>{{this.redFighter.record}}</p>
      </v-col>
      <v-col mb="4" align="center" id="compareFightersStatsMiddleColumn">
        <p>Age</p>
        <p>Stance</p>
        <p>Weight class</p>
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
      <v-col mb="4" v-if="this.blueFighter == ''"></v-col>
      <v-col mb="4" align="left" v-if="this.blueFighter != ''">
        <p>{{this.blueFighter.birth}}</p>
        <p>{{this.blueFighter.stance }}</p>
        <p>{{this.blueFighter.weightClass}}</p>
        <p>{{this.blueFighter.record}}</p>
      </v-col>
    </v-row>
    <v-divider class="dividerWithPadding" />
    <v-row>
      <v-col xs="2" v-if="this.redFighter == ''"></v-col>
      <v-col xs="3" align="right" v-if="this.redFighter != ''">
        <div class="hidden-xs-only">
          <p v-bind:style="{color:heightColor[0]}">
            <v-icon color="yellow" small v-if="dominantRedHeight">mdi-crown</v-icon>
            {{this.redFighter.height }}/{{this.redFighter.heightCm}}cm
          </p>
          <p v-bind:style="{color:weightColor[0]}">
            <v-icon color="yellow" small v-if="dominantRedWeight">mdi-crown</v-icon>
            {{this.redFighter.weight }}lbs/{{this.redFighter.weightKg }}kg
          </p>
          <p v-bind:style="{color:reachColor[0]}">
            <v-icon color="yellow" small v-if="dominantRedReach">mdi-crown</v-icon>
            {{this.redFighter.reach }}"/{{this.redFighter.reachCm}}cm
          </p>
          <p v-bind:style="{color:tdAccColor[0]}">
            <v-icon color="yellow" small v-if="dominantRedTakedownAccuracy">mdi-crown</v-icon>
            {{this.redFighter.takedownAccuracy }}%
          </p>
          <p v-bind:style="{color:tdAvgColor[0]}">
            <v-icon color="yellow" small v-if="dominantRedTakedownAverage">mdi-crown</v-icon>
            {{this.redFighter.takedownAverage }}
          </p>
          <p v-bind:style="{color:submissionAvgColor[0]}">
            <v-icon color="yellow" small v-if="dominantRedSubmissionAverage">mdi-crown</v-icon>
            {{this.redFighter.submissionAverage }}
          </p>
        </div>
        <div class="hidden-sm-and-up">
          <p class="mt-1 smallFont" v-bind:style="{color:heightColor[0]}">
            {{this.redFighter.height }}/{{this.redFighter.heightCm}}cm
            <v-icon color="yellow" small v-if="dominantRedHeight">mdi-crown</v-icon>
          </p>
          <p class="mt-3 smallFont" v-bind:style="{color:weightColor[0]}">
            {{this.redFighter.weight }}lbs/{{this.redFighter.weightKg }}kg
            <v-icon color="yellow" small v-if="dominantRedWeight">mdi-crown</v-icon>
          </p>
          <p class="mt-7 smallFont" v-bind:style="{color:reachColor[0]}">
            {{this.redFighter.reach }}"/{{this.redFighter.reachCm}}cm
            <v-icon color="yellow" small v-if="dominantRedReach">mdi-crown</v-icon>
          </p>
          <p class="mt-5 smallFont" v-bind:style="{color:tdAccColor[0]}">
            {{this.redFighter.takedownAccuracy }}%
            <v-icon color="yellow" small v-if="dominantRedTakedownAccuracy">mdi-crown</v-icon>
          </p>
          <p class="mt-6 smallFont" v-bind:style="{color:tdAvgColor[0]}">
            {{this.redFighter.takedownAverage }}
            <v-icon color="yellow" small v-if="dominantRedTakedownAverage">mdi-crown</v-icon>
          </p>
          <p class="mt-1 smallFont" v-bind:style="{color:submissionAvgColor[0]}">
            {{this.redFighter.submissionAverage }}
            <v-icon color="yellow" small v-if="dominantRedSubmissionAverage">mdi-crown</v-icon>
          </p>
        </div>
      </v-col>
      <v-col xs="8" align="center" id="compareFightersStatsMiddleColumn">
        <p>Height</p>
        <p>Weight</p>
        <p>Reach</p>
        <div>
          <p class="hidden-sm-and-down">
            Takedown Accuracy
            <v-tooltip right>
              <template v-slot:activator="{ on }">
                <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
              </template>
              <span>
                The number of takedowns landed divided by the number
                attempted takedowns
              </span>
            </v-tooltip>
          </p>
        </div>
        <p class="hidden-md-and-up">
          TA
          <v-tooltip left>
            <template v-slot:activator="{ on }">
              <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
            </template>
            <span>
              Takedown Accuracy:
              The number of takedowns landed divided by the number
              attempted takedowns
            </span>
          </v-tooltip>
        </p>
        <p class="hidden-sm-and-down">Average takedown attempts per match</p>
        <p class="hidden-md-and-up">
          ATAPM
          <v-tooltip left>
            <template v-slot:activator="{ on }">
              <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
            </template>
            <span>Average takedown attempts per match</span>
          </v-tooltip>
        </p>

        <p class="hidden-sm-and-down">Average submission attempts per match</p>
        <p class="hidden-md-and-up">
          ASAPM
          <v-tooltip left>
            <template v-slot:activator="{ on }">
              <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
            </template>
            <span>Average submisssion attempts per match</span>
          </v-tooltip>
        </p>
      </v-col>
      <v-col xs="2" v-if="this.blueFighter == ''"></v-col>
      <v-col xs="3" align="left" v-if="this.blueFighter != ''">
        <div class="hidden-xs-only">
          <p v-bind:style="{color:heightColor[1]}">
            {{this.blueFighter.height }}/{{this.blueFighter.heightCm}}cm
            <v-icon color="yellow" small v-if="dominantBlueHeight">mdi-crown</v-icon>
          </p>
          <p v-bind:style="{color:weightColor[1]}">
            {{this.blueFighter.weight }}lbs/{{this.blueFighter.weightKg}}kg
            <v-icon color="yellow" small v-if="dominantBlueWeight">mdi-crown</v-icon>
          </p>
          <p v-bind:style="{color:reachColor[1]}">
            {{this.blueFighter.reach }}"/{{this.blueFighter.reachCm}}cm
            <v-icon color="yellow" small v-if="dominantBlueReach">mdi-crown</v-icon>
          </p>
          <p v-bind:style="{color:tdAccColor[1]}">
            {{this.blueFighter.takedownAccuracy }}%
            <v-icon color="yellow" small v-if="dominantBlueTakedownAccuracy">mdi-crown</v-icon>
          </p>
          <p class="mt-2" v-bind:style="{color:tdAvgColor[1]}">
            {{this.blueFighter.takedownAverage }}
            <v-icon color="yellow" small v-if="dominantBlueTakedownAverage">mdi-crown</v-icon>
          </p>
          <p class="mt-2" v-bind:style="{color:submissionAvgColor[1]}">
            {{this.blueFighter.submissionAverage }}
            <v-icon color="yellow" small v-if="dominantBlueSubmissionAverage">mdi-crown</v-icon>
          </p>
        </div>
        <div class="hidden-sm-and-up">
          <p class="smallFont mt-1" v-bind:style="{color:heightColor[1]}">
            {{this.blueFighter.height }}/{{this.blueFighter.heightCm}}cm
            <v-icon color="yellow" small v-if="dominantBlueHeight">mdi-crown</v-icon>
          </p>
          <p class="smallFont mt-3" v-bind:style="{color:weightColor[1]}">
            {{this.blueFighter.weight }}lbs/{{this.blueFighter.weightKg}}kg
            <v-icon color="yellow" small v-if="dominantBlueWeight">mdi-crown</v-icon>
          </p>
          <p class="smallFont mt-7" v-bind:style="{color:reachColor[1]}">
            {{this.blueFighter.reach }}"/{{this.blueFighter.reachCm}}cm
            <v-icon color="yellow" small v-if="dominantBlueReach">mdi-crown</v-icon>
          </p>
          <p class="smallFont mt-5" v-bind:style="{color:tdAccColor[1]}">
            {{this.blueFighter.takedownAccuracy }}%
            <v-icon color="yellow" small v-if="dominantBlueTakedownAccuracy">mdi-crown</v-icon>
          </p>
          <p class="mt-6 smallFont" v-bind:style="{color:tdAvgColor[1]}">
            {{this.blueFighter.takedownAverage }}
            <v-icon color="yellow" small v-if="dominantBlueTakedownAverage">mdi-crown</v-icon>
          </p>
          <p class="mt-1 smallFont" v-bind:style="{color:submissionAvgColor[1]}">
            {{this.blueFighter.submissionAverage }}
            <v-icon color="yellow" small v-if="dominantBlueSubmissionAverage">mdi-crown</v-icon>
          </p>
        </div>
      </v-col>
    </v-row>
    <v-divider class="dividerWithPadding" />
  </v-container>
</template>


<script>
export default {
  name: "comparison-table",
  components: {},
  data() {
    return {
      redFighter: this.$store.state.chosenRedFighter,
      blueFighter: this.$store.state.chosenBlueFighter,
      heightColor: ["", ""],
      weightColor: ["", ""],
      reachColor: ["", ""],
      tdAvgColor: ["", ""],
      tdAccColor: ["", ""],
      submissionAvgColor: ["", ""],
      dominantBlueHeight: false,
      dominantRedHeight: false,
      dominantBlueWeight: false,
      dominantRedWeight: false,
      dominantBlueReach: false,
      dominantRedReach: false,
      dominantBlueTakedownAccuracy: false,
      dominantRedTakedownAccuracy: false,
      dominantBlueTakedownAverage: false,
      dominantRedTakedownAverage: false,
      dominantBlueSubmissionAverage: false,
      dominantRedSubmissionAverage: false
    };
  },
  methods: {
    setfighter: function(fighter) {
      if (fighter[0] == "red") {
        this.redFighter = fighter[1];
      } else if (fighter[0]) {
        this.blueFighter = fighter[1];
      }

      // Set green color on dominantattributes
      if (this.redFighter != "" && this.blueFighter != "") {
        let dominantColor = "white";
        let normalColor = "white";

        // Height color
        if (this.redFighter.heightCm > this.blueFighter.heightCm) {
          this.heightColor = [dominantColor, normalColor];
          this.dominantRedHeight = true;
          this.dominantBlueHeight = false;
        } else if (this.redFighter.heightCm < this.blueFighter.heightCm) {
          this.heightColor = [normalColor, dominantColor];
          this.dominantBlueHeight = true;
          this.dominantRedHeight = false;
        } else {
          this.heightColor = [normalColor, normalColor];
          this.dominantBlueHeight = false;
          this.dominantRedHeight = false;
        }

        // Weight color
        if (this.redFighter.weight > this.blueFighter.weight) {
          this.dominantRedWeight = true;
          this.dominantBlueHeight = false;
          this.weightColor = [dominantColor, normalColor];
        } else if (this.redFighter.weight < this.blueFighter.weight) {
          this.weightColor = [normalColor, dominantColor];
          this.dominantRedWeight = false;
          this.dominantBlueWeight = true;
        } else {
          this.weightColor = [normalColor, normalColor];
          this.dominantRedWeight = false;
          this.dominantBlueWeight = false;
        }

        // Reach color
        if (this.redFighter.reachCm > this.blueFighter.reachCm) {
          this.dominantRedReach = true;
          this.dominantBlueReach = false;
          this.reachColor = [dominantColor, normalColor];
        } else if (this.redFighter.reachCm < this.blueFighter.reachCm) {
          this.reachColor = [normalColor, dominantColor];
          this.dominantRedReach = false;
          this.dominantBlueReach = true;
        } else {
          this.reachColor = [normalColor, normalColor];
          this.dominantBlueReach = false;
          this.dominantRedReach = false;
        }

        // Takedown Average color
        if (
          this.redFighter.takedownAverage > this.blueFighter.takedownAverage
        ) {
          this.tdAvgColor = [dominantColor, normalColor];
          this.dominantRedTakedownAverage = true;
          this.dominantBlueTakedownAverage = false;
        } else if (
          this.redFighter.takedownAverage < this.blueFighter.takedownAverage
        ) {
          this.tdAvgColor = [normalColor, dominantColor];
          this.dominantBlueTakedownAverage = true;
          this.dominantRedTakedownAverage = false;
        } else {
          this.tdAvgColor = [normalColor, normalColor];
          this.dominantRedTakedownAverage = false;
          this.dominantBlueTakedownAverage = false;
        }

        // Takedown Average color
        if (
          this.redFighter.takedownAccuracy > this.blueFighter.takedownAccuracy
        ) {
          this.tdAccColor = [dominantColor, normalColor];
          this.dominantRedTakedownAccuracy = true;
          this.dominantBlueTakedownAccuracy = false;
        } else if (
          this.redFighter.takedownAccuracy < this.blueFighter.takedownAccuracy
        ) {
          this.tdAccColor = [normalColor, dominantColor];
          this.dominantRedTakedownAccuracy = false;
          this.dominantBlueTakedownAccuracy = true;
        } else {
          this.tdAccColor = [normalColor, normalColor];
          this.dominantRedTakedownAccuracy = false;
          this.dominantBlueTakedownAccuracy = false;
        }

        // Submission Average color
        if (
          this.redFighter.submissionAverage > this.blueFighter.submissionAverage
        ) {
          this.submissionAvgColor = [dominantColor, normalColor];
          this.dominantRedSubmissionAverage = true;
          this.dominantBlueSubmissionAverage = false;
        } else if (
          this.redFighter.submissionAverage < this.blueFighter.submissionAverage
        ) {
          this.submissionAvgColor = [normalColor, dominantColor];
          this.dominantRedSubmissionAverage = false;
          this.dominantBlueSubmissionAverage = true;
        } else {
          this.submissionAvgColor = [normalColor, normalColor];
          this.dominantRedSubmissionAverage = false;
          this.dominantBlueSubmissionAverage = false;
        }
      }
    },
    compareStats: function(stat1, stat2) {
      if (stat1 > stat2) {
        return true;
      } else {
        return false;
      }
    },
    compareFightersInUpcomingEvent: function(fighters) {
      this.redFighter = this.$store.state.chosenRedFighter;
      this.blueFighter = this.$store.state.chosenBlueFighter;

      let dominantColor = "#8aff73";
      let normalColor = "white";

      // Height color
      if (this.redFighter.height > this.blueFighter.height) {
        this.heightColor = [dominantColor, normalColor];
      } else if (this.redFighter.height < this.blueFighter.height) {
        this.heightColor = [normalColor, dominantColor];
      } else {
        this.heightColor = [normalColor, normalColor];
      }

      // Weight color
      if (this.redFighter.weight > this.blueFighter.weight) {
        this.weightColor = [dominantColor, normalColor];
      } else if (this.redFighter.weight < this.blueFighter.weight) {
        this.weightColor = [normalColor, dominantColor];
      } else {
        this.weightColor = [normalColor, normalColor];
      }

      // Reach color
      if (this.redFighter.reachCm > this.blueFighter.reachCm) {
        this.reachColor = [dominantColor, normalColor];
      } else if (this.redFighter.reachCm < this.blueFighter.reachCm) {
        this.reachColor = [normalColor, dominantColor];
      } else {
        this.reachColor = [normalColor, normalColor];
      }

      // Takedown Average color
      if (this.redFighter.takedownAverage > this.blueFighter.takedownAverage) {
        this.tdAvgColor = [dominantColor, normalColor];
      } else if (
        this.redFighter.takedownAverage < this.blueFighter.takedownAverage
      ) {
        this.tdAvgColor = [normalColor, dominantColor];
      } else {
        this.tdAvgColor = [normalColor, normalColor];
      }

      // Takedown Average color
      if (
        this.redFighter.takedownAccuracy > this.blueFighter.takedownAccuracy
      ) {
        this.tdAccColor = [dominantColor, normalColor];
      } else if (
        this.redFighter.takedownAccuracy < this.blueFighter.takedownAccuracy
      ) {
        this.tdAccColor = [normalColor, dominantColor];
      } else {
        this.tdAccColor = [normalColor, normalColor];
      }

      // Submission Average color
      if (
        this.redFighter.submissionAverage > this.blueFighter.submissionAverage
      ) {
        this.submissionAvgColor = [dominantColor, normalColor];
      } else if (
        this.redFighter.submissionAverage < this.blueFighter.submissionAverage
      ) {
        this.submissionAvgColor = [normalColor, dominantColor];
      } else {
        this.submissionAvgColor = [normalColor, normalColor];
      }
    }
  },
  mounted() {
    this.$root.$on("setfighter", fighter => {
      this.setfighter(fighter);
    });

    this.$root.$on("compareFightersInUpcomingEvent", fighters => {
      this.compareFightersInUpcomingEvent(fighters);
    });
    this.$root.$on("setfighterExternal", fighter => {
      this.setfighter(fighter);
    });
  }
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/comparisonTable.css";
</style>
