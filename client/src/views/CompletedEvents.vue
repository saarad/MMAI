<template>
    <v-layout class="background">
        <v-flex>
            <completed-events-card> </completed-events-card>
        </v-flex>
    </v-layout>
</template>

<script>
import Vue from 'vue'
import CompletedEventsCard from '../components/CompletedEventsCard.vue'
export default Vue.extend({
    name: "CompletedEvents",
    components: {
        CompletedEventsCard
    }
})
</script>

<style>
@import "../../public/css/app.css";
</style>