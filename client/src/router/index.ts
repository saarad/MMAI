import Vue from 'vue'
import VueRouter from 'vue-router'

//adding lazy loading by using dynamic imports for importing components
const Home = () => import('../views/Home.vue')
const UpcomingEvents = () => import ('../views/UpcomingEvents.vue')
const FighterLib = () =>  import('../views/FighterLib.vue')
const CompareFighters = () =>  import('../views/CompareFighters.vue')
const CompletedEvents = () => import('../views/CompletedEvents.vue')

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home,
  },
  {
    path: '/upcomingEvents',
    name: 'upcomingEvents',
    component: UpcomingEvents,
  },{
    path: '/completedEvents',
    name: 'completedEvents',
    component: CompletedEvents
  }, {
    path: '/fighterLib',
    name: 'fighterLib',
    component: FighterLib,

  }, {
    path: '/compareFighters',
    name: 'compareFighters',
    component: CompareFighters,

  }

]

// @ts-ignore 
const router = new VueRouter({
  mode: 'history',
  routes: routes
})


export default router