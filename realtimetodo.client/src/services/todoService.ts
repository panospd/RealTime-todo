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

        this.connection.on("updatedToDoList", (values: any[]) => {
            console.log(values)
            this.events.emit("updatedToDoList", values)
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
}

export const ConnectionServices: PluginObject<any> = {
    install(Vue: VueConstructor<Vue>, option: any | undefined) {
        Vue.$connectionService = new ToDoService();
        Vue.prototype.$connectionService = Vue.$connectionService
    }
}