<template>
  <v-bottom-navigation
    v-model="bottomNav"
    dark
    fixed
    class="navigationMenuDark hidden-md-and-up"
    id="bottomNav"
  >
    <v-btn to="/" value="home">
      <span>Home</span>
      <v-icon>mdi-home</v-icon>
    </v-btn>

    <v-btn to="/upcomingEvents" value="upcomingEvents">
      <span>Events</span>
      <v-icon>mdi-map-marker</v-icon>
    </v-btn>

    <v-btn to="/completedEvents" value="completedEvents">
      <span>Completed</span>
      <v-icon>mdi-calendar-multiple-check</v-icon>
    </v-btn>

    <v-btn to="/compareFighters" value="compareFighters">
      <span>Compare</span>
      <v-icon>mdi-compare</v-icon>
    </v-btn>

    <v-btn to="/fighterLib" value="fighterLibrary">
      <span>Library</span>
      <v-icon>mdi-book-open-page-variant</v-icon>
    </v-btn>
  </v-bottom-navigation>
</template>

<script>
export default {
  name: "bottom-navigation-menu",
  components: {},
  data() {
    return {
      bottomNav: "",
      hello: ""
    };
  }
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/navigationMenu.css";
</style>
