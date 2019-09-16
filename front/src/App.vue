<template>
  <v-app id="inspire">

    <Loader :is-visible="isLoading"></Loader>

    <v-navigation-drawer v-if="isLogged" v-model="drawer" app>
      <v-list dense>
        <v-list-item @click="navigate('/')">
          <v-list-item-action>
            <v-icon>mdi-home</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="navigate('/user')" v-if="user && user.perfil == 'DeptPessoal'">
          <v-list-item-action>
            <v-icon>mdi-contact-mail</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Usuários</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="navigate('/resource')" v-if="user && user.perfil == 'CtrlProducao'">
          <v-list-item-action>
            <v-icon>mdi-animation</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Recursos</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="navigate('/vote')">
          <v-list-item-action>
            <v-icon>mdi-chart-pie</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Votar</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="navigate('/vote/audit')" v-if="user && user.perfil == 'CtrlProducao'">
          <v-list-item-action>
            <v-icon>mdi-account-details</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Auditar Votos</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="navigate('/vote/result')">
          <v-list-item-action>
            <v-icon>mdi-poll-box</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Recursos Mais votados</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="logout">
          <v-list-item-action>
            <v-icon>mdi-exit-to-app</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Logout</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app color="indigo" dark>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" v-if="isLogged"></v-app-bar-nav-icon>
      <v-toolbar-title>System. <span v-if="isLogged">Bem vindo,  {{user.nome}}</span></v-toolbar-title>
    </v-app-bar>

    <v-content>
      <router-view></router-view>
    </v-content>

    <v-footer color="indigo" app>
      <span class="white--text">Alterdata &copy; 2019</span>
    </v-footer>
  </v-app>
</template>

<script>
import Loader from '@/components/Loader'

export default {
  name: 'App',
  components: {
    Loader
  },
  computed: {
    isLogged: function () {
      return this.$store.getters.isAuthenticated
    },
    user: function () {
      return this.$store.getters.user
    }
  },
  data: () => ({
    drawer: false,
    isLoading: false
  }),
  methods: {
    navigate: function (url) {
      this.$router.push(url)
    },
    logout: function () {
      this.drawer = false
      this.$notification.success('Logout com sucesso, mas não demore a voltar... x)', { timer: 10 })
      this.$store.dispatch('logout').then(() => {
        this.$router.push('/login')
      })
    },
    enableInterceptor () {
      this.$http.interceptors.request.use(
        config => {
          this.isLoading = true
          return config
        },
        error => {
          this.isLoading = false
          return Promise.reject(error)
        }
      )
      this.$http.interceptors.response.use(response => {
        this.isLoading = false
        return response
      }, err => {
        this.isLoading = false
        if (err.response.status === 401) {
          this.$notification.error('Sua sessão expirou, é hora de fazer login novamente :)', { timer: 10 })
          this.$store.dispatch('logout').then(() => {
            this.$router.push('/login')
          })
        }
        return Promise.reject(err)
      })
    }
  },
  mounted: function () {
    this.enableInterceptor()
  }
}
</script>
