import Vue, { PluginObject, VueConstructor } from "vue"
import { EventEmitter } from "events"
import { HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr"

export default class ToDoService {
    connection: signalR.HubConnection;
    events: EventEmitter
    constructor() {
        this.events = new EventEmitter();
        this.connection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Trace)
            .withUrl("/hubs/todo")
            .build();

        this.connection.on("UpdatedToDoList", (values: any[]) => {
            this.events.emit("updatedToDoList", values)
        })

        this.connection.on("UpdatedListData", (value: any) => {
            this.events.emit("updatedListData", value)
        })
    }

    async start() {
        await this.connection.start();
    }

    getLists() {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("GetLists");         
        }

        setTimeout(() => this.getLists(), 500);
    }

    getListData(id: number) {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("GetList", id);         
        }

        setTimeout(() => this.getListData(id), 500);
    }

    addToDoItem(listid: number, text: string) {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("AddToDoItem", listid, text);         
        }

        setTimeout(() => this.addToDoItem(listid, text), 500);
    }

    subscribeToCountUpdates() {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("SubscribeToCountUpdates");         
        }

        setTimeout(() => this.subscribeToCountUpdates(), 500);
    }

    unSubscribeFromCountUpdates() {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("UnSubscribeFromCountUpdates");         
        }

        setTimeout(() => this.unSubscribeFromCountUpdates(), 500);
    }

    subscribeToListUpdates (id: number) {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("SubscribeToListUpdates", id);         
        }

        setTimeout(() => this.subscribeToListUpdates(id), 500);
    }

    unSubscribeFromListUpdates (id: number) {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("UnSubscribeFromListUpdates", id);         
        }

        setTimeout(() => this.unSubscribeFromListUpdates(id), 500);
    }

    toggleToDoItem (listId: number, itemId: number) {
        if (this.connection.state === HubConnectionState.Connected) {
            return this.connection.send("ToggleToDoItem", listId, itemId);         
        }

        setTimeout(() => this.toggleToDoItem(listId, itemId), 500);
    }
}

export const ConnectionServices: PluginObject<any> = {
    install(Vue: VueConstructor<Vue>, option: any | undefined) {
        Vue.$connectionService = new ToDoService();
        Vue.prototype.$connectionService = Vue.$connectionService
    }
}