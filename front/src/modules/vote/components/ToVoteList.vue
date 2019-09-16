<template>
  <v-container
    fluid
  >
    <h1>Resultado</h1>
    <v-row
      align="center"
      justify="center"
    >
      <v-col
        cols="12"
        sm="12"
        md="12"
      >
        <v-data-table
          :headers="headers"
          :items="votes"
          :options.sync="options"
          :server-items-length="total"
          class="elevation-1"
        >
          <template v-slot:item.action="{ item }">
            <v-icon
              color="success"
              @click="vote(item)"
            >
              mdi-check-all
            </v-icon>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
    <v-dialog
      v-model="dialog"
      width="500"
    >
      <v-card>
        <v-card-title class="headline">Você tem certeza?</v-card-title>

        <v-card-text>
            Deseja votar em "{{itemselected.descricao}}"?
            <br>Em caso afirmativo, faça o seu comentário e clique em 'Enviar voto'.
            <br> Lembre-se: você só pode votar uma vez em cada recurso
            <v-form lazy-validation>

              <v-textarea
                label="Comente:"
                name="comments"
                type="text"
                v-model="comments"
                :rules="commentsRules"
              ></v-textarea>

            </v-form>
        </v-card-text>

        <v-card-actions>
          <div class="flex-grow-1"></div>

          <v-btn
            outlined class="ml-4" large color="error" dark
            @click="dialog = false"
          >
            Espere, mudei de ideia
          </v-btn>

          <v-btn
            outlined class="ml-4" large color="success" dark
            @click="confirmVote()"
          >
            Enviar voto
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>

export default {
  name: 'UserList',
  props: {
    source: String
  },
  components: {
    //
  },
  data: () => ({
    commentsRules: [
      v => !!v || 'Sua opinião é importante!'
    ],
    itemselected: {},
    comments: '',
    votes: [],
    headers: [
      { text: 'Descrição', value: 'descricao' },
      { text: 'Vote', value: 'action', sortable: false }
    ],
    total: 0,
    dialog: false,
    options: {
      sortBy: [],
      sortDesc: []
    },
    filter: {
      per_page: 25,
      page: 1
    }
  }),
  watch: {
    options: {
      handler () {
        this.load()
      },
      deep: true
    }
  },
  methods: {
    load: function () {
      this.filter.porPagina = this.options.itemsPerPage
      this.filter.paginaAtual = this.options.page
      let query = JSON.stringify(this.filter)
      this.$http.get('/api/voto', { params: { query } })
        .then(resp => {
          this.votes = resp.data.dados
          this.total = resp.data.total
        })
        .catch(err => {
          console.log(err)
        })
    },
    edit (item) {
      this.$router.push(`/user/${item.id}`)
    },
    vote (item) {
      this.itemselected = item
      this.dialog = true
    },
    confirmVote () {
      if (this.comments.length === 0) {
        this.$notification.error('O Comentário é obrigatório!', { timer: 10 })
      }

      const data = {
        recursoId: this.itemselected.id,
        comentario: this.comments
      }
      this.$http.post('/api/voto', data)
        .then(resp => {
          this.dialog = false
          this.comments = ''
          this.$notification.success('Prontinho! Processamos o seu voto. Lembrando que vc só pode votar uma vez em cada Recurso propost ;)', { timer: 10 })
          this.load()
        })
        .catch(err => {
          this.$notification.error('Não conseguimos processar o seu voto, será q poderia entrar em contato com o suporte?', { timer: 10 })
          console.log(err)
        })
    }
  }
}
</script>

<style>

</style>
