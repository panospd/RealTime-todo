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
            <router-link :to="{ name: 'List', params: { listId: l.id }}">{{l.listName}}</router-link>
          </td>
          <td>{{l.completed}}</td>
          <td>{{l.pending}}</td>
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

  demoList() {
    this.lists.push({
      id: 1,
      listName: "Foo",
      completed: 5,
      pending: 10
    })
    this.lists.push({
      id: 2,
      listName: "Bar",
      completed: 15,
      pending: 10
    })
    this.lists.push({
      id: 3,
      listName: "Faa",
      completed: 0,
      pending: 2
    })
    this.lists.push({
      id: 4,
      listName: "Laa",
      completed: 30,
      pending: 20
    })
  }

  created() {
    this.demoList();
  }
}
</script>
