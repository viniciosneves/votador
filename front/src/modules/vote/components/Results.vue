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
          class="elevation-1"
        >
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>

export default {
  name: 'Results',
  props: {
    source: String
  },
  components: {
    //
  },
  data: () => ({
    votes: [],
    headers: [
      { text: 'Descrição', value: 'descricao' },
      { text: 'Total de Votos', value: 'votos' }
    ]
  }),
  methods: {
    load: function () {
      this.$http.get('/api/resultado')
        .then(resp => {
          this.votes = resp.data
        })
        .catch(err => {
          console.log(err)
        })
    }
  },
  mounted () {
    this.load()
  }
}
</script>

<style>

</style>
