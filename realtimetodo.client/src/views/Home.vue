<template>
  <div class="home">
    <h1>To Do List</h1>
    <br>
    <table>
      <thead>
        <tr>
          <th>List Name</th>
          <th>Pending</th>
          <th>Complete</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="l in lists" :key="l.id">
          <td>
            <router-link :to="{ name: 'List', params: { listId: l.id }}">{{l.name}}</router-link>
          </td>
          <td> {{l.pending }} </td>
          <td> {{l.completed }} </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({})
export default class Home extends Vue {
  lists: any[] = [];

  async created() {
    this.$connectionService.events.on("updatedToDoList", (values: any[]) => {
      this.lists = values
    })

    this.$connectionService.getLists()
    this.$connectionService.subscribeToCountUpdates();
  }

  beforeDestroy(){
    this.$connectionService.unSubscribeFromCountUpdates();
  }
}
</script>
