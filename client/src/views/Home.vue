<template>
  <v-layout class="background">
    <v-content transition="scale-transition">
      <section id="hero">
        <v-row no-gutters>
          <v-img
            src="https://i.imgur.com/WsKhOEq.jpg"
            :min-height="'calc(100vh - ' + $vuetify.application.top + 'px)'"
            :max-height="'calc(120vh + ' + $vuetify.application.top + 'px)'"
          >
            <v-theme-provider dark>
              <v-container fill-height>
                <v-row align="center" class="white--text mx-auto" justify="center">
                  <v-col class="white--text text-center" cols="12" tag="h1">
                    <span
                      class="font-weight-light"
                      :class="[$vuetify.breakpoint.smAndDown ? 'display-1' : 'display-2']"
                    >WELCOME TO</span>

                    <br />

                    <v-expand-transition>
                      <span
                        :class="[$vuetify.breakpoint.smAndDown ? 'display-3': 'display-4']"
                        class="font-weight-black"
                      >M M A I</span>
                    </v-expand-transition>
                  </v-col>

                  <v-btn class="align-self-end" fab outlined @click="$vuetify.goTo('#about-me')">
                    <v-icon>mdi-chevron-double-down</v-icon>
                  </v-btn>
                </v-row>
              </v-container>
            </v-theme-provider>
          </v-img>
        </v-row>
      </section>

      <section id="about-me">
        <div class="py-12"></div>

        <v-container class="text-center">
          <v-lazy align="center" :options="{threshold: .1}">
            <v-img
              max-width="50vh"
              transition="scale-transition"
              origin="center center"
              src="https://i.imgur.com/8oD77St.png"
            ></v-img>
          </v-lazy>

          <v-responsive
            class="mx-auto title font-weight-light mb-8"
            max-width="720"
          >MMAI lets you compare the statistics of a wide range of UFC-athletes, as well as giving you access to our machine learning algorithm that predicts the outcome of a fight between the chosen fighters.</v-responsive>

          <v-btn to="/compareFighters" large rounded color="red lighten-2">Get started</v-btn>
        </v-container>

        <div class="py-12"></div>
      </section>

      <section id="features" class="grey lighten-3">
        <div class="py-12"></div>

        <v-container class="text-center">
          <h2 id="ourFeatures" class="darkText display-2 font-weight-bold mb-3">Features</h2>

          <v-responsive class="mx-auto mb-12" width="56">
            <v-divider class="mb-1"></v-divider>

            <v-divider></v-divider>
          </v-responsive>

          <v-row>
            <v-col v-for="({ icon, title, text, src }, i) in features" :key="i" cols="12" md="4">
              <v-hover v-slot:default="{ hover }">
                <v-card
                  :to="src"
                  :elevation="hover ? 16 : 2"
                  class="py-12 px-4"
                  color="grey lighten-5"
                  flat
                >
                  <v-theme-provider dark>
                    <div>
                      <router-link :to="src">
                        <v-hover v-slot:default="{ hover }">
                          <v-avatar
                            :color="hover ? 'blue' : 'blue lighten-2'"
                            :transition="hover ? 'scale-transition' : ''"
                            size="88"
                            :to="src"
                          >
                            <v-icon large v-text="icon"></v-icon>
                          </v-avatar>
                        </v-hover>
                      </router-link>
                    </div>
                  </v-theme-provider>

                  <v-card-title
                    class="darkText justify-center font-weight-black text-uppercase"
                    v-text="title"
                  ></v-card-title>

                  <v-card-text class="darkText subtitle-1" v-text="text"></v-card-text>
                </v-card>
              </v-hover>
            </v-col>
          </v-row>
        </v-container>

        <div class="py-12"></div>
      </section>

      <section id="stats">
        <v-parallax
          :height="$vuetify.breakpoint.smAndDown ? 700 : 500"
          src="https://img.bleacherreport.net/img/images/photos/002/277/877/hi-res-143237499_crop_north.jpg?h=533&w=800&q=70&crop_x=center&crop_y=top"
        >
          <v-container fill-height>
            <v-row class="mx-auto">
              <v-col v-for="[value, title] of stats" :key="title" cols="12" md="4">
                <div class="text-center">
                  <div class="display-3 font-weight-black mb-4" v-text="value"></div>

                  <div class="title font-weight-regular text-uppercase" v-text="title"></div>
                </div>
              </v-col>
            </v-row>
          </v-container>
        </v-parallax>
      </section>
      <!--
      <section id="ethics" class="white darken-1">
        <div class="py-12"></div>
        <v-container class="text-center">
          <v-responsive class="darkText mx-auto title font-weight-light mb-8" max-width="720">
            This service is not intended to be used for gambling in any shape or form, and we disclaim responsibility for any economic loss or gain you may recieve by doing so.
            Gambling is really addictive and is associated with financial problems. We recommend that the users of this web site refrains from gambling.
            <br />
            <br />If you're under 18 and are gambling, we warn you that this greatly increases the chance of becoming a problem gambler.
          </v-responsive>
        </v-container>
        <div class="py-12"></div>
      </section>
      -->
      
      <section id="affiliates"
               class="white">
          <div class="py-12"></div>
          <v-container fill-height class="text-center">
            <v-row
              justify="center">
              <h2 id="ourAffiliates" class="darkText display-2 font-weight-bold mb-3">Affiliated bookmakers</h2>
            </v-row>
            <v-responsive
              class="mx-auto mb-12"
              width="56"
            >
              <v-divider class="mb-1"></v-divider>

              <v-divider></v-divider>
            </v-responsive>
            
            <v-row class="mx-auto">

              <v-col
                v-for="[key, image, link] of affiliates"
                :key="key"
                cols="12"
                md="3"
              >
                <v-hover class="affiliates" v-slot:default="{ hover }">
                  <v-card
                  :elevation="hover ? 16 : 2"
                    class="py-12 px-4"
                    color="dark lighten-5"
                    flat
                  >
                    <div class="text-center">
                      <v-img 
                      :src="image"
                      @click="goToUrl(link)"></v-img>
                    </div>
                  </v-card>
                </v-hover>
                
              </v-col>
            </v-row>
           
          </v-container>
          <div class="py-12"></div>
      </section>

      
      <v-sheet id="contact" dark tag="section" tile>
        <div class="py-12"></div>

        <v-container>
          <h2 class="display-2 font-weight-bold mb-3 text-uppercase text-center">Contact Us</h2>

          <v-responsive class="mx-auto mb-12" width="56">
            <v-divider class="mb-1"></v-divider>

            <v-divider></v-divider>
          </v-responsive>

          <v-theme-provider light>
            <v-row>
              <v-col cols="12">
                <v-text-field flat label="Name*" solo v-model="contactUsName"></v-text-field>
              </v-col>

              <v-col cols="12">
                <v-text-field flat label="Email*" solo v-model="contactUsFrom"></v-text-field>
              </v-col>

              <v-col cols="12">
                <v-text-field flat label="Subject*" solo v-model="contactUsSubject"></v-text-field>
              </v-col>

              <v-col cols="12">
                <v-textarea flat label="Message*" solo v-model="contactUsText"></v-textarea>
              </v-col>

              <v-col class="mx-auto" cols="auto">
                <v-btn dark color="red lighten-2" x-large @click="contactUs()">Submit</v-btn>
              </v-col>
              <v-snackbar v-model="snackbar" :timeout="2000" :color="snackbarColor">
                <span color="danger">{{snackbarText}}</span>
                <v-btn color="white" text @click="snackbar = false">Close</v-btn>
                <v-progress-circular
                  v-if="mailInProgress == true"
                  :size="50"
                  color="primary"
                  indeterminate
                ></v-progress-circular>
              </v-snackbar>
            </v-row>
          </v-theme-provider>
        </v-container>
        <div class="py-12"></div>
      </v-sheet>
    </v-content>
  </v-layout>
</template>

<script>
import axios from "axios";
const gradients = [
  ["#222"],
  ["#42b3f4"],
  ["red", "orange", "yellow"],
  ["red"],
  ["#f72047", "#ffd200", "#1feaea"],
  ["red", "red", "orange"]
];
export default {
  name: "home",
  components: {},
  mounted: function() {
    if (process.env.NODE_ENV === "development") {
      this.apiString = "https://localhost:7225/graphql";
    } else {
      this.apiString =
        "https://mmai-api.azurewebsites.net/graphql";
    }
    this.scrapeData();
    window.scrollTo({
      top: 0,
      left: 0,
      behavior: "smooth"
    });
  },
  methods: {
    goToUrl(url) {
      window.location = url;
    },

    scrapeData: async function(){
      var today = new Date();
      var dd = today.getDate();
      var mm = today.getMonth()+1; //typescript array for month starting at 0
      var yyyy = today.getFullYear();
      //formatting
      if(dd<10) 
        {
            dd='0'+dd;
        } 

        if(mm<10) 
        {
            mm='0'+mm;
        } 
      var date = dd+'.'+mm+'.'+yyyy;
      const res = await axios.post(
        this.apiString,
        {
          query: `mutation{
                    scrapeUpcomingEvents
                  }`,
          variables: {
              date: date
            }
        }
      );
      return res.data.data.scrapeData;
    },

    contactUs: async function(){
      this.mailInProgress = true;

      if (
        /\S/.test(this.contactUsName) &&
        /\S/.test(this.contactUsFrom) &&
        /\S/.test(this.contactUsSubject) &&
        /\S/.test(this.contactUsText)
      ) {
        const res = await axios.post(this.apiString, {
          query: `
                    mutation contactUs($name: String!, $from: String!, $subject: String!, $text: String!) {
                        contactUs(name: $name, from: $from, subject: $subject, text: $text)
                    } 
               `,
          variables: {
            name: this.contactUsName,
            from: this.contactUsFrom,
            subject: this.contactUsSubject,
            text: this.contactUsText
          }
        });

        this.contactUsName = "";
        this.contactUsFrom = "";
        this.contactUsSubject = "";
        this.contactUsText = "";
        this.snackbarColor = "success";
        this.snackbarText = "Email successfully sent.";
        this.snackbar = true;
        this.mailInProgress = false;

        this.$vuetify.goTo(0);
        return res.data.data.contactUs;
      } else {
        this.snackbarText =
          "Please fill out all the fields in the contact form.";
        this.snackbarColor = "red";
        this.snackbar = true;
        this.mailInProgress = false;
        return;
      }
    }
  },
  data: () => ({
    snackbar: false,
    snackbarText: "Email successfully sent",
    snackbarColor: "danger",
    mailInProgress: false,
    contactUsName: "",
    contactUsFrom: "",
    contactUsSubject: "",
    contactUsText: "",
    features: [
      {
        icon: "mdi-map-marker",
        title: "Upcoming Events",
        text:
          "Get an overview of upcoming UFC events and predict the respective matches before they happen.",
        src: "/upcomingEvents"
      },
      {
        icon: "mdi-calendar-multiple-check",
        title: "Completed events",
        text:
          "Get an overview of completed UFC events dating back to 1993 with complete statistics of each match.",
        src: "/completedEvents"
      },
      {
        icon: "mdi-compare",
        title: "Compare Fighters",
        text:
          "Compare statistics of two fighters and simulate the fight! Who do you think will win?",
        src: "/compareFighters"
      },
      {
        icon: "mdi-book-open-page-variant",
        title: "Fighter Library",
        text:
          "Inspect the statistics of a specific athlete, see results of previous matches and more.",
        src: "/fighterLib"
      }
    ],
    articles: [
      {
        src:
          "https://transformationtools.no/siteassets/media/artikelbilleder-770x507/ai.jpg",
        title: "title",
        text: "text"
      }
    ],
    stats: [
      ["74%", "Prediction accuracy"],
      ["3500+", "Athletes"],
      ["5500+", "Previous fights"]
    ],
    hover: false,
    affiliates: [
      [
        1,
        "https://cdnroute.bpsgameserver.com/echo-cdn-origin/content/betsafe/sites/4/2017/10/logo_betsafe_white-responsive.png",
        "http://record.betsafe.com/_IeFgwgHetDVOhkZVvtkCVGNd7ZgqdRLk/1/"
      ],
      [
        2,
        "https://cdnroute.bpsgameserver.com/echo-cdn-origin/content/betsafe/sites/4/2017/10/logo_betsafe_white-responsive.png",
        "http://record.betsafe.com/_IeFgwgHetDVOhkZVvtkCVGNd7ZgqdRLk/1/"
      ],
      [
        3,
        "https://cdnroute.bpsgameserver.com/echo-cdn-origin/content/betsafe/sites/4/2017/10/logo_betsafe_white-responsive.png",
        "http://record.betsafe.com/_IeFgwgHetDVOhkZVvtkCVGNd7ZgqdRLk/1/"
      ],
      [
        4,
        "https://cdnroute.bpsgameserver.com/echo-cdn-origin/content/betsafe/sites/4/2017/10/logo_betsafe_white-responsive.png",
        "http://record.betsafe.com/_IeFgwgHetDVOhkZVvtkCVGNd7ZgqdRLk/1/"
      ]
    ]
  })
};
</script>
<style>
@import "../../public/css/app.css";
@import "../../public/css/home.css";
</style>
