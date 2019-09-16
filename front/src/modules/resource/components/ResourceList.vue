<template>
  <v-container
    fluid
  >
    <v-row
      align="center"
      justify="center"
    >
      <v-col cols="12">
        <v-btn color="indigo" fab dark @click="$router.push('/resource/create')">
          <v-icon>mdi-plus</v-icon>
        </v-btn>
      </v-col>
      <v-col
        cols="12"
        sm="12"
        md="12"
      >
        <v-data-table
          :headers="headers"
          :items="resources"
          :options.sync="options"
          :server-items-length="total"
          class="elevation-1"
        >
          <template v-slot:item.action="{ item }">
            <v-icon
              class="mr-2"
              color="info"
              @click="edit(item)"
            >
              mdi-circle-edit-outline
            </v-icon>
            <v-icon
              color="error"
              @click="destroy(item)"
            >
              mdi-delete
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
         O recurso <strong>{{itemselected.descricao}}</strong> será deletado. Deseja prosseguir?
        </v-card-text>

        <v-card-actions>
          <div class="flex-grow-1"></div>

          <v-btn
            outlined class="ml-4" large color="error" dark
            @click="dialog = false"
          >
            Espere, vou verificar
          </v-btn>

          <v-btn
            outlined class="ml-4" large color="success" dark
            @click="confirmDelete()"
          >
            Sim, prossiga
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>

export default {
  name: 'ResourceList',
  props: {
    source: String
  },
  components: {
    //
  },
  data: () => ({
    itemselected: {},
    resources: [],
    headers: [
      { text: 'Descrição', value: 'descricao' },
      { text: 'Ações', value: 'action', sortable: false }
    ],
    total: 0,
    dialog: false,
    options: {
      sortBy: [],
      sortDesc: []
    },
    filter: {
      per_page: 25,
      page: 1,
      sort_ascending: true
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
      this.$http.get('/api/recurso', { params: { query } })
        .then(resp => {
          this.resources = resp.data.dados
          this.total = resp.data.total
        })
        .catch(err => {
          console.log(err)
        })
    },
    edit (item) {
      this.$router.push(`/resource/${item.id}`)
    },
    destroy (item) {
      this.itemselected = item
      this.dialog = true
    },
    confirmDelete () {
      this.$http.delete(`/api/recurso/${this.itemselected.id}`)
        .then(resp => {
          this.$notification.success('Prontinho ;)', { timer: 10 })
          this.dialog = false
          this.load()
        })
        .catch(err => {
          console.log(err)
        })
    }
  }
}
</script>

<style>

</style>
