<template>
  <div id="fabContainer">
    <v-btn
      class="hidden-sm-and-down"
      v-scroll="onScroll"
      v-show="fab"
      fab
      dark
      bottom
      fixed
      right
      color="red lighten-2"
      @click="toTop"
      id="scroll-to-top-fab"
    >
      <v-icon>mdi-chevron-double-up</v-icon>
    </v-btn>
    <v-btn
      class="hidden-md-and-up"
      v-scroll="onScroll"
      v-show="fab"
      fab
      dark
      bottom
      fixed
      right
      color="red lighten-2"
      @click="toTop"
      id="scroll-to-top-fab"
      small
    >
      <v-icon>mdi-chevron-double-up</v-icon>
    </v-btn>
  </div>
</template>

<script>
export default {
  name: "scroll-to-top",
  components: {},
  methods: {
    onScroll(e) {
      if (typeof window === "undefined") return;
      const top = window.pageYOffset || e.target.scrollTop || 0;
      this.fab = top > 20;
    },
    toTop() {
      this.$vuetify.goTo(0);
    }
  },
  data() {
    return {
      fab: true
    };
  }
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/scrollFab.css";
</style>
