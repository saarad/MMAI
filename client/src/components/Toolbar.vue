<template>
  <v-card flat class="overflow-visible">
    <v-toolbar fixed text dark class="navigationMenuDark hidden-sm-and-down">
      <router-link to="/">
        <v-toolbar-title color="red lighten-2" id="toolbartitle">M M A I</v-toolbar-title>
      </router-link>
      <v-spacer />
      <v-toolbar-items>
        <v-btn v-for="item in nav" :key="item.icon" :to="item.to" :title="item.title" text>
          <v-icon>{{item.icon}}</v-icon>
          <span class="toolbarLink">{{ item.text }}</span>
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
  </v-card>
</template>

<script>
export default {
  name: "toolbar",
  components: {},
  data() {
    return {
      bottomNav: "",
      hello: "",
      nav: [
        {
          icon: "mdi-map-marker",
          text: "Upcoming Events",
          title: "Get an overview of upcoming UFC events",
          active: false,
          to: "/upcomingEvents"
        },
        {
          icon: "mdi-calendar-multiple-check",
          text: "Completed Events",
          title: "Get an overview of completed UFC events",
          active: false,
          to: "/completedEvents"
        },
        {
          icon: "mdi-compare",
          text: "Compare Fighters",
          title: "Compare two fighters against eachother",
          active: false,
          to: "/compareFighters"
        },
        {
          icon: "mdi-book-open-page-variant",
          text: "Fighter Library",
          title: "See stats about one specific figther",
          active: false,
          to: "/fighterLib"
        }
      ]
    };
  }
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/toolbar.css";
</style>
