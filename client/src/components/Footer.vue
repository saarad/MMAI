<template>
  <v-footer padless id="footer" class="hidden-sm-and-down" dense>
    <v-col class="text-center" cols="12">
      {{ new Date().getFullYear() }} —
      <strong>MMAI</strong>
    </v-col>
  </v-footer>
</template>

<script>
export default {
  name: "mmai-footer",
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
      fab: false
    };
  }
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/footer.css";
</style>
