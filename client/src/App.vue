<template>
  <v-app dark>
    <toolbar />
    <router-view class="content"></router-view>
    <scroll-to-top />
    <bottom-navigation-menu />
    <mmai-footer />
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import BottomNavigationMenu from "./components/BottomNavigationMenu.vue";
import SideNavigationMenu from "./components/SideNavigationMenu.vue";
import Toolbar from "./components/Toolbar.vue";
import MmaiFooter from "./components/Footer.vue";
import ScrollToTop from "./components/ScrollToTopFab.vue";

export default Vue.extend({
  name: "App",

  components: {
    BottomNavigationMenu,
    Toolbar,
    MmaiFooter,
    ScrollToTop
  },

  data: () => ({}),
  methods: {}
});
</script>
<style>
@import "../public/css/app.css";
</style>
