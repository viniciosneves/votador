import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import baseAPI from './http'
import VueNotification from '@kugatsu/vuenotification'
import moment from 'moment'

Vue.prototype.$http = baseAPI
Vue.use(VueNotification, {
  timer: 20
})

Vue.prototype.$moment = moment

let stringuser = localStorage.getItem('user')
if (stringuser && stringuser !== 'undefined') {
  store.commit('auth_success', JSON.parse(stringuser))
}

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
