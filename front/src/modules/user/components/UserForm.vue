<template>
  <v-container>
        <v-row
          align="center"
          justify="center"
        >
          <v-col
            cols="12"
            sm="12"
            md="6"
          >
            <h1>usuário</h1>
            <v-form lazy-validation>

              <v-text-field
                label="Nome"
                name="name"
                type="text"
                v-model="user.nome"
                :rules="nameRules"
              ></v-text-field>

              <v-text-field
                label="E-mail"
                name="login"
                type="text"
                v-model="user.email"
                :rules="emailRules"
              ></v-text-field>

              <v-text-field
                id="password"
                label="Senha"
                name="password"
                type="password"
                v-model="user.senha"
                :rules="passwordRules"
              ></v-text-field>

              <v-select
                label="Perfil"
                :items="profiles"
                item-text="nome"
                item-value="id"
                v-model="user.perfil"
              ></v-select>

              <v-btn color="primary" @click="submit">Salvar</v-btn>

            </v-form>

      </v-col>
    </v-row>
  </v-container>
</template>

<script>

export default {
  name: 'UserList',
  props: {
    source: String
  },
  mounted () {
    if (this.$route.params.id) {
      this.load()
    }
    this.$http.get('/api/perfil/')
      .then(resp => {
        this.profiles = resp.data
        this.passwordRules = []
      })
      .catch(err => {
        console.log(err)
      })
  },
  components: {
    //
  },
  data: () => ({
    profiles: [],
    user: {},
    nameRules: [
      v => !!v || 'Nome é obrigatório'
    ],
    passwordRules: [
      v => !!v || 'Senha é obrigatório'
    ],
    emailRules: [
      v => !!v || 'E-mail é obrigatório',
      v => /.+@.+\..+/.test(v) || 'E-mail precisa ser válido'
    ]
  }),
  methods: {
    load () {
      this.$http.get(`/api/usuario/${this.$route.params.id}`)
        .then(resp => {
          this.user = resp.data
          this.passwordRules = []
        })
        .catch(err => {
          console.log(err)
        })
    },
    submit () {
      if (this.$route.params.id) {
        this.put()
      } else {
        this.post()
      }
    },
    post () {
      this.$http.post('/api/usuario/', this.user)
        .then(resp => {
          this.$notification.success('Prontinho ;)', { timer: 10 })
          this.$router.push('/user')
        })
        .catch(err => {
          this.$notification.error('Algo deu errado e precisamos que vc acione o suporte :(', { timer: 10 })
          console.log(err)
        })
    },
    put () {
      this.$http.put(`/api/usuario/${this.$route.params.id}`, this.user)
        .then(resp => {
          this.$notification.success('Prontinho ;)', { timer: 10 })
          this.$router.push('/user')
        })
        .catch(err => {
          this.$notification.error('Algo deu errado e precisamos que vc acione o suporte :(', { timer: 10 })
          console.log(err)
        })
    }
  }
}
</script>

<style>

</style>
