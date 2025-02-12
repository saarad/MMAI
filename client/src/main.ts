import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify';
import Vue from 'vue'
import VueGoogleCharts from 'vue-google-charts'

Vue.config.productionTip = false

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')



Vue.use(VueGoogleCharts);