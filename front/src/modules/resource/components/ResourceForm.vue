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
            <h1>Recurso</h1>
            <v-form lazy-validation>

              <v-textarea
                label="Descrição"
                name="descricao"
                type="text"
                v-model="resource.descricao"
                :rules="descricaoRules"
              ></v-textarea>

              <v-btn color="primary" @click="submit">Salvar</v-btn>

            </v-form>

      </v-col>
    </v-row>
  </v-container>
</template>

<script>

export default {
  name: 'ResourceList',
  props: {
    source: String
  },
  mounted () {
    if (this.$route.params.id) {
      this.load()
    }
  },
  components: {
    //
  },
  data: () => ({
    profiles: [],
    resource: {},
    descricaoRules: [
      v => !!v || 'Descrição é obrigatório'
    ]
  }),
  methods: {
    load () {
      this.$http.get(`/api/recurso/${this.$route.params.id}`)
        .then(resp => {
          this.resource = resp.data
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
      this.$http.post('/api/recurso/', this.resource)
        .then(resp => {
          this.$notification.success('Prontinho ;)', { timer: 10 })
          this.$router.push('/resource')
        })
        .catch(err => {
          this.$notification.error('Algo deu errado e precisamos que vc acione o suporte :(', { timer: 10 })
          console.log(err)
        })
    },
    put () {
      this.$http.put(`/api/recurso/${this.$route.params.id}`, this.resource)
        .then(resp => {
          this.$notification.success('Prontinho ;)', { timer: 10 })
          this.$router.push('/resource')
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
