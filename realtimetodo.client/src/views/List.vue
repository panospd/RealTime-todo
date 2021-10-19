<template>
<div>
    <h1>List: {{ list.name }}</h1>
    <hr>
    <table>
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th>Task</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="i in list.items" :key="i.id">
                <td>
                    <input 
                        type="checkbox" 
                        v-model="i.isCompleted"
                        @change="toggleToDoItem(i.id)">
                </td>
                <td>{{i.text}}</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <input type="text" v-model.trim="newItemText">
                    <button @click="addNewItem">+</button>
                </td>
            </tr>
        </tbody>
    </table>
</div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({})
export default class List extends Vue {
    listId = -1
    newItemText = ""
    list: any = {
        name: "",
        items: []
    }

    toggleToDoItem(itemId: number) {
        this.$connectionService.toggleToDoItem(this.listId, itemId)
    }

    addNewItem() {
        if (!this.newItemText)
        {
            return;
        }

        this.$connectionService.addToDoItem(this.listId, this.newItemText)
        this.newItemText = ""
    }

    created() {
        this.listId = parseInt(this.$route.params.listId);

        this.$connectionService.events.on("updatedListData", (data: any) => {
            this.list = data
        })

        this.$connectionService.getListData(this.listId)
        this.$connectionService.subscribeToListUpdates(this.listId)
    }

    beforeDestroy(){
    this.$connectionService.unSubscribeFromListUpdates();
  }
}
</script>

<style scoped>

</style>
