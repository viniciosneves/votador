<template>
  <v-container
    fluid
  >
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
        <template v-slot:body="{ items }">
          <tbody>
            <tr v-for="item in items" :key="item.name">
              <td>{{ item.recurso.descricao }}</td>
              <td>{{ item.usuario.nome }}</td>
              <td>{{ item.comentario }}</td>
              <td>{{ timestamp(item.createdAt) }}</td>
            </tr>
          </tbody>
        </template>
        </v-data-table>
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
  components: {
    //
  },
  data: () => ({
    itemselected: {},
    votes: [],
    headers: [
      { text: 'Descricao', value: 'recurso.descricao' },
      { text: 'Usuário', value: 'usuario.nome' },
      { text: 'Comentário', value: 'comentario' },
      { text: 'Data', value: 'createdAt' }
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
      this.$http.get('/api/auditoria', { params: { query } })
        .then(resp => {
          this.votes = resp.data.dados
          this.total = resp.data.total
        })
        .catch(err => {
          console.log(err)
        })
    },
    timestamp (datestring) {
      return this.$moment(datestring).format('DD/MM/YYYY H:mm:ss')
    }
  }
}
</script>

<style>

</style>
