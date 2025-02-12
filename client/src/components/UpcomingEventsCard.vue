<template>
  <v-layout justify-center align-center>
    <v-card class="py-0 px-0 ma-0 pa-0 test" min-width="100%">
      <v-row v-if="!progress">
        <v-col align="center">
          <v-progress-circular
            :size="200"
            :width="10"
            color="red"
            indeterminate
            v-if="!progress"
          >Loading..</v-progress-circular>
        </v-col>
      </v-row>
      <v-list-item>
        <v-list-item-content>
          <v-tabs
            class="py-0 px-0 ma-0 pa-0"
            dark
            center-active
            show-arrows
            slider-color="red lighten-1"
            color="red lighten-1"
            background-color="#353b4a"
            mobile-break-point="1500"
            slider-size="4"
          >
            <v-tab
              class="tabs__div"
              v-for="item in upcomingEvents"
              :key="item.eventName"
            >{{item.eventName}}</v-tab>
            <v-tab-item v-for="item in upcomingEvents" :key="item.eventName" dark>
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
                        <h5 class="hidden-sm-and-down">Location</h5>
                      </div>
                      {{item.location}}
                    </v-col>
                  </v-row>
                </v-card-title>
                <v-card-text class="py-0 px-2 ma-0 pa-0">
                  <div v-if="showModal">
                    <upcoming-fight-modal></upcoming-fight-modal>
                  </div>
                  <v-row>
                    <v-col align="left" class="leftCol">
                      <div>
                        <h4 class="redText">
                          Red corner
                          <v-tooltip left>
                            <template v-slot:activator="{ on }">
                              <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
                            </template>
                            <span>The favourite of the match</span>
                          </v-tooltip>
                        </h4>
                      </div>
                    </v-col>
                    <v-col align="right" class="rightCol">
                      <div>
                        <h4 class="blueText">
                          Blue corner
                          <v-tooltip right>
                            <template v-slot:activator="{ on }">
                              <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
                            </template>
                            <span>The underdog of the match</span>
                          </v-tooltip>
                        </h4>
                      </div>
                    </v-col>
                  </v-row>
                  <v-data-table
                    disable-pagination
                    :items="item.fights"
                    item-key="fightName"
                    hide-default-footer
                    class="py-0 px-1 ma-1 pa-0 test"
                  >
                    <template v-slot:item="props">
                      <tr>
                        <td align="left">
                          <div>
                            <v-avatar size="80" class="hidden-sm-and-up">
                              <img class="fighterImage" :src="props.item.redimage" />
                            </v-avatar>
                            <h5 class="hidden-xs-only weightClass">{{props.item.weightClass}}</h5>
                          </div>
                          <div>
                            <h4 class="hidden-xs-only">{{props.item.fighter1}}</h4>
                            <h6 class="hidden-sm-and-up">{{props.item.fighter1}}</h6>
                          </div>
                        </td>
                        <td align="left" class="hidden-xs-only">
                          <v-avatar size="90">
                            <img class="fighterImage" :src="props.item.redImage" />
                          </v-avatar>
                        </td>
                        <td class="hidden-md-and-up" align="center">
                          <div>
                            <v-btn
                              light
                              width="90"
                              height="20"
                              @click="compareFightersInUpcomingEvent(props.item.fighter1, props.item.fighter2)"
                            >
                              <h6>Compare</h6>
                            </v-btn>
                            <v-btn
                              light
                              width="90"
                              height="20"
                              @click="changeModalState(props.item.fighter1, props.item.fighter2)"
                            >
                              <h6>Quick predict</h6>
                            </v-btn>
                          </div>
                        </td>
                        <td align="center" class="hidden-sm-and-down">
                          <v-btn
                            light
                            @click="compareFightersInUpcomingEvent(props.item.fighter1, props.item.fighter2)"
                          >Compare</v-btn>
                        </td>
                        <td class="hidden-sm-and-down">
                          <v-btn
                            @click="changeModalState(props.item.fighter1, props.item.fighter2)"
                            light
                          >Quick predict</v-btn>
                        </td>
                        <td align="right" class="hidden-xs-only">
                          <v-avatar size="90">
                            <img class="fighterImage" :src="props.item.blueImage" />
                          </v-avatar>
                        </td>
                        <td align="right">
                          <v-avatar size="80" class="hidden-sm-and-up">
                            <img class="fighterImage" :src="props.item.blueImage" />
                          </v-avatar>
                          <h5 class="hidden-xs-only weightClass">{{props.item.weightclass}}</h5>
                          <div>
                            <h4 class="hidden-xs-only">{{props.item.fighter2}}</h4>
                            <h6 class="hidden-sm-and-up">{{props.item.fighter2}}</h6>
                          </div>
                        </td>
                      </tr>
                    </template>
                  </v-data-table>
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
// @ is an alias to /src

import axios from "axios";
import { router } from "../router/index";
import { EventBus } from "../plugins/vuetify";
import UpcomingFightModal from "./UpcomingFightModal.vue";

export default {
  name: "upcoming-events-card",
  components: {
    "upcoming-fight-modal": UpcomingFightModal
  },

  data() {
    return {
      upcomingEvents: [],
      fighter1: [],
      fighter2: [],
      apiString: "https://localhost:7225/graphql",
      progress: false
    };
  },

  computed: {
    showModal() {
      return this.$store.state.showModal;
    }
  },

  methods: {
    compareFightersInUpcomingEvent: async function(fighter1, fighter2) {
      this.$store.commit("setCompareFighters", [fighter1, fighter2]);
      this.$router.push("/compareFighters");
    },
    setUpcomingFightersInModal: async function(fighter1, fighter2) {
      this.$store.commit("setModalUpcomingFighters", [fighter1, fighter2]);
    },
    changeModalState: async function(fighter1, fighter2) {
      this.$store.commit("toggleModal", [fighter1, fighter2]);
    },

    getUpcomingEvents: async function() {
      const res = await axios.post(this.apiString, {
        query: `
                query{ 
                  upcomingEvents {
                        eventName
                        location
                        date
                        fights{
                          fighter1
                          fighter2
                          weightClass
                          redImage
                          blueImage
                        }
                  }
                } 
            `
      });

      this.upcomingEvents = res.data.data.upcomingEvents;
      this.progress = true;
    }
  },

  mounted: function() {
    if (process.env.NODE_ENV === "development") {
      this.apiString = "https://localhost:7225/graphql";
    } else {
      this.apiString =
        "https://mmai-api.azurewebsites.net/graphql";
    }
    this.getUpcomingEvents();
  }
};
</script>
<style>
@import "../../public/css/upcomingEventCard.css";
@import "../../public/css/app.css";
</style>