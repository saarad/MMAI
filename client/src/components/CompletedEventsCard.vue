<template>
  <v-layout justify-center align-center>
    <v-card class="py-0 px-0 ma-0 pa-0 test" min-width="100%">
      <v-row v-if="!progressEvents">
        <v-col align="center">
          <v-progress-circular
            :size="200"
            :width="10"
            color="red"
            indeterminate
            v-if="!progressEvents"
          >
            Loading..
          </v-progress-circular>
        </v-col>
      </v-row>
      <v-list-item>
        <v-list-item-content>
          <v-tabs
            class="py-0 px-0 ma-0 pa-0"
            dark
            center-active
            v-model="activeTab"
            show-arrows
            slider-color="red lighten-1"
            color="red lighten-1"
            background-color="#353b4a"
            mobile-break-point="1500"
            slider-size="4"
          >
            <v-tab
              v-for="item in completedEvents"
              :key="item.eventName"
            >{{item.eventName}}</v-tab>
            
            <v-tab-item v-for="item in completedEvents" :key="item.eventName" dark>
              <v-card flat class="test">
                <v-card-title>
                  <v-row>
                    <v-col align="center">
                      <div>
                        <h5 class="hidden-sm-and-down">Date</h5>
                      </div>
                      {{item.date}}
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col align="center">
                      <div>
                        <h5 class="hidden-sm-and-down">Title</h5>
                      </div>
                        {{item.eventName}}
                    </v-col>
                  </v-row>
                </v-card-title>
                <v-card-text class="py-0 px-2 ma-0 pa-0">
                  <v-row v-if="!progressMatches">
                    <v-col align="center">
                      <v-progress-circular
                        :size="200"
                        :width="10"
                        color="red"
                        indeterminate
                        v-if="!progressMatches"
                      >
                        Loading..
                      </v-progress-circular>
                    </v-col>
                  </v-row>
                  <match-history :matchHistory="completedMatches"/>
                </v-card-text>
              </v-card>
            </v-tab-item>
          </v-tabs>
        </v-list-item-content>
      </v-list-item>
    </v-card>
  </v-layout>
</template>

<script>
import Vue from 'vue'
import axios from 'axios'
import MatchHistory from "./MatchHistory/MatchHistory.vue";
export default Vue.extend({
    name: 'completed-event-card',
    components: {
        MatchHistory
    },
    data(){
        return{
            completedEvents: [],
            completedMatches: [],
            apiString: "",
            progressMatches: false,
            progressEvents: false,
            activeTab: 0,
            expanded: [],
        }
    },
    methods: {

        getCompletedEvents: async function(){
          this.progressEvents = false;
            const res = await axios.post(this.apiString, {
                query: `
                    query{
                        completedEvents{
                            eventName
                            date
                        }
                    }
                `
            });

            this.completedEvents = res.data.data.completedEvents;
            await this.getOneCompleteEvent(this.completedEvents[this.activeTab].eventName, this.completedEvents[this.activeTab].date);
            this.progressEvents = true;
        },
        getOneCompleteEvent: async function(eventName, date){
            this.progressMatches = false;
            const res = await axios.post(this.apiString, {
                query: `
                        query oneCompletedEvent($eventName: String!, $date: String!){
                            oneCompletedEvent(eventName: $eventName, date: $date){
                              redName
                              blueName
                              referee
                              time
                              method
                              weightClass
                              round
                              winner
                              eventName
                              redImage
                              blueImage
                              totalRed
                              totalBlue
                              strikesRed
                              strikesBlue
                              tdRed
                              tdBlue
                              passRed
                              passBlue
                              revRed
                              revBlue
                              subRed
                              subBlue
                              clinchRed
                              clinchBlue
                              groundRed
                              groundBlue
                              distanceRed
                              distanceBlue
                              headRed
                              headBlue
                              bodyRed
                              bodyBlue
                              legRed
                              legBlue
                            }
                        }
                `,
                variables: {
                            eventName: eventName,
                            date: date
                }
            });
            this.completedMatches = res.data.data.oneCompletedEvent;
            this.progressMatches = true;
        }

    },
    mounted: function(){
        if (process.env.NODE_ENV === "development") {
            this.apiString = "https://localhost:7225/graphql";
        } else {
            this.apiString = "https://mmai-api.azurewebsites.net/graphql";
        }
        this.getCompletedEvents();
    },
    watch: {
        activeTab(){
            this.completedMatches = [];
            this.getOneCompleteEvent(this.completedEvents[this.activeTab].eventName, this.completedEvents[this.activeTab].date);
        }
    }    
})
</script>

<style>
@import "../../public/css/upcomingEventCard.css";
@import "../../public/css/app.css";
</style>