<template>
  <v-layout class="background">
    <v-flex>
      <upcoming-events-card></upcoming-events-card>
    </v-flex>
  </v-layout>
</template>

<script>
import UpcomingEventsCard from "../components/UpcomingEventsCard.vue";

export default {
  name: "upcomingEvents",
  components: {
    UpcomingEventsCard
  },
  data() {
    return {
      hello: ""
    };
  }
};
</script>
<style>
@import "../../public/css/app.css";
</style>
