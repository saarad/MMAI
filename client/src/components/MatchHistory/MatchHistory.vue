<template>
<v-expansion-panels>
  <v-flex class="background">
    <v-expansion-panel v-for="match in matchHistory"
      v-bind:key="match.redName + match.blueName + match.eventName" >

      <v-expansion-panel-header style="background: #1f222a;">
        <match-history-row
        :match="match"
        />
      </v-expansion-panel-header>
      <v-divider/>
      <v-expansion-panel-content eager style="background: #1f222a">
        <v-row>
          <v-col xs="4" align="center">
            {{match.totalRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Total strikes
          </v-col>
          <v-col xs="4" align="center">
              {{match.totalBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.strikesRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Significant Strikes
          </v-col>
          <v-col xs="4" align="center">
              {{match.strikesBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.tdRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Takedowns
          </v-col>
          <v-col xs="4" align="center">
              {{match.tdBlue}}
          </v-col>
        </v-row>
        
        <v-row>
          <v-col xs="4" align="center">
            {{match.passRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Pass
          </v-col>
          <v-col xs="4" align="center">
              {{match.passBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.revRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Rev
          </v-col>
          <v-col xs="4" align="center">
              {{match.revBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.subRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              <p class="hidden-xs-only"> Submission attempts</p>
              <p class="hidden-sm-and-up">
                SA
                <v-tooltip top>
                  <template v-slot:activator="{ on }">
                    <v-icon small v-on="on">mdi-help-circle-outline</v-icon>
                  </template>
                  <span>
                    Submission Attemps
                  </span>
                </v-tooltip>
              </p>
          </v-col>
          <v-col xs="4" align="center">
              {{match.subBlue}}
          </v-col>
        </v-row>
        <v-divider></v-divider>
        <v-row>
          <v-col xs="12" align="center">
             <h2> Strikes by position <v-icon large>mdi-mixed-martial-arts</v-icon> </h2>
          </v-col>
        </v-row>
        <v-divider></v-divider>
        <v-row>
          <v-col xs="4" align="center">
            {{match.clinchRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Clinch
          </v-col>
          <v-col xs="4" align="center">
              {{match.clinchBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.groundRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Ground
          </v-col>
          <v-col xs="4" align="center">
              {{match.groundBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.distanceRed}}
          </v-col>
          
          <v-col xs="4" id="columnDivider" align="center">
              Distance
          </v-col>
          <v-col xs="4" align="center">
              {{match.distanceBlue}}
          </v-col>
        </v-row>

        <v-divider></v-divider>
        <v-row>
          <v-col xs="12" align="center">
            <h2>Strikes by target <v-icon large> mdi-human</v-icon></h2>
          </v-col>
        </v-row>
        <v-divider></v-divider>
        <v-row>
          <v-col xs="4" align="center">
            {{match.headRed}}
          </v-col>  
          <v-col xs="4" id="columnDivider" align="center">
              Head
          </v-col>
          <v-col xs="4" align="center">
              {{match.headBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.bodyRed}}
          </v-col>
          <v-col xs="4" id="columnDivider" align="center">
              Body
          </v-col>
          <v-col xs="4" align="center">
              {{match.bodyBlue}}
          </v-col>
        </v-row>

        <v-row>
          <v-col xs="4" align="center">
            {{match.legRed}}
          </v-col>
          <v-col xs="4" id="columnDivider" align="center">
              Leg
          </v-col>
          <v-col xs="4" align="center">
              {{match.legBlue}}
          </v-col>
        </v-row>
      </v-expansion-panel-content>
    </v-expansion-panel>
  </v-flex>
 
</v-expansion-panels>
  
</template>

<script>
import MatchHistoryRow from "./MatchHistoryRow.vue";

export default {
  name: "match-history",
  props: { matchHistory: Array },
  components: {
    MatchHistoryRow
  },
  data() {
    return {};
  },
  mounted() {}
};
</script>
<style>
@import "../../../public/css/app.css";
@import "../../../public/css/matchHistory.css";
@import "../../../public/css/compareFighters.css";

#columnDivider{
  border-right: solid 1px  var(--main-bg-dark);
  border-left: solid 1px  var(--main-bg-dark);
}
</style>
