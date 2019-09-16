import Vue from 'vue'
import Vuex from 'vuex'
import baseAPI from '@/http'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: localStorage.getItem('token') || '',
    status: '',
    user: {}
  },
  mutations: {
    auth_request: (state) => {
      state.status = 'loading'
    },
    auth_success: (state, user) => {
      state.status = 'success'
      state.token = user.token
      state.user = user
    },
    auth_error: (state) => {
      state.status = 'error'
    },
    logout: (state) => {
      state.status = ''
      state.token = ''
      state.user = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    }
  },
  actions: {
    login: ({ commit }, user) => {
      return new Promise((resolve, reject) => {
        commit('auth_request')

        const data = {
          email: user.email,
          senha: user.password
        }

        baseAPI.post('api/auth', data)
          .then(resp => {
            const token = resp.data.token
            const user = resp.data.user
            localStorage.setItem('token', token)
            baseAPI.defaults.headers.common['Authorization'] = `Bearer ${token}`
            user.token = token
            localStorage.setItem('user', JSON.stringify(user))
            commit('auth_success', user)
            resolve(resp)
          })
          .catch(err => {
            commit('auth_error')
            localStorage.removeItem('token')
            reject(err)
          })
      })
    },
    logout: ({ commit }) => {
      return new Promise((resolve, reject) => {
        commit('logout')
        localStorage.removeItem('token')
        localStorage.removeItem('user')
        delete baseAPI.defaults.headers.common['Authorization']
        resolve()
      })
    }
  },
  getters: {
    isAuthenticated: state => !!state.token,
    authStatus: state => state.status,
    user: state => state.user
  }
})
