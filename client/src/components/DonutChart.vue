<template>
  <v-container align="center" justify="center">
    <h3 id="donutTitle" :class="fighterColor=='red' ? 'redFighter' : 'blueFighter'">{{title}}</h3>
    <v-progress-circular
      id="donutChart"
      :size="size"
      :width="width"
      rotate="270"
      :class="fighterColor=='red' ? 'redFighter' : 'blueFighter'"
      :value="value"
    >{{value}} {{text}}</v-progress-circular>
  </v-container>
</template>

<script>
export default {
  name: "donut-chart",
  components: {},
  props: {
    fighterColor: String,
    title: String,
    size: String,
    width: String,
    value: Number,
    text: String
  },
  data() {
    return {};
  }
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/donutChart.css";
</style>