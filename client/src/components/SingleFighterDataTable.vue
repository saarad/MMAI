<template>
  <v-container
    v-if="this.fighter != ''"
    class="singleFighterDataTable"
    transition="scale-transition"
    origin="center center"
    :key="this.fighter.name+'dataTable'"
  >
    <v-divider class="dividerWithPadding" />
    <v-row>
      <v-col md="3" sm="1" xs="1" class="hidden-xs-only" align="right"></v-col>
      <v-col md="6" sm="10" xs="10" align="center" id="compareFightersStatsMiddleColumn">
        <v-row>
          <v-col align="left">
            <p>Age</p>
            <p>Stance</p>
            <p>Weight class</p>
            <p>Record</p>
          </v-col>

          <v-col align="right">
            <p>{{this.fighter.birth}}</p>
            <p>{{this.fighter.stance }}</p>
            <p>{{this.fighter.weightClass}}</p>
            <p>{{this.fighter.record}}</p>
          </v-col>
        </v-row>
      </v-col>
      <v-col md="3" sm="1" xs="1" class="hidden-xs-only"></v-col>
    </v-row>

    <v-divider class="dividerWithPadding" />
    <v-row>
      <v-col md="3" sm="1" align="right" class="hidden-xs-only"></v-col>
      <v-col md="6" sm="10" align="center" id="compareFightersStatsMiddleColumn">
        <v-row>
          <v-col align="left" cols="7">
            <p>Height</p>
            <p>Weight</p>
            <p>Reach</p>
            <p>Takedown accuracy</p>
            <p>Average takedown attempts per match</p>
            <p>Average submission attempts per match</p>
          </v-col>
          <v-col align="right" cols="5">
            <p>{{this.fighter.height }} / {{this.fighter.heightCm}} cm</p>
            <p>{{this.fighter.weight }} lbs / {{this.fighter.weightKg}} kg</p>
            <p>{{this.fighter.reach }}" / {{this.fighter.reachCm}} cm</p>
            <p>{{this.fighter.takedownAccuracy }}%</p>
            <p class="hidden-xs-only">{{this.fighter.takedownAverage }}</p>
            <p class="hidden-xs-only" mt->{{this.fighter.submissionAverage }}</p>
            <p class="hidden-sm-and-up mt-8">{{this.fighter.takedownAverage }}</p>
            <p class="hidden-sm-and-up mt-8">{{this.fighter.submissionAverage }}</p>
          </v-col>
        </v-row>
      </v-col>
      <v-col md="3" sm="1" class="hidden-xs-only"></v-col>
    </v-row>
    <v-divider class="dividerWithPadding" />
  </v-container>
</template>

<script>
export default {
  name: "single-fighter-data-table",
  components: {},
  data() {
    return {
      fighter: ""
    };
  },
  methods: {
    setfighter: function(fighter) {
      this.fighter = fighter[1];
    }
  },
  mounted() {
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
@import "../../public/css/app.css";
@import "../../public/css/singleFighterDataTable.css";
</style>
