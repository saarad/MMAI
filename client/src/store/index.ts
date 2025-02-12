import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    chosenRedFighter: '',
    chosenBlueFighter: '',
    compareFighters: '',
    showModal: false,

  },
  mutations: {
    setRedFighter(state, payload) {
      state.chosenRedFighter = payload;
    },
    setBlueFighter(state, payload) {
      state.chosenBlueFighter = payload;
    },
    setCompareFighters(state, payload) {
      state.compareFighters = payload;
    },
    toggleModal(state, payload) {
      state.showModal = !state.showModal;
      state.compareFighters = payload;
    },
    closeModal(state) {
      state.showModal = !state.showModal;
    }

  },
  getters: {
    showModal: state => {
      return state.showModal
    }
  },
  actions: {
  },
  modules: {
  }
})
