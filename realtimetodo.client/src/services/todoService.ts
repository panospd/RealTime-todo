import Vue, { PluginObject, VueConstructor } from "vue"
import {HubConnectionBuilder, LogLevel } from "@microsoft/signalr"

export default class ToDoService {
    connection: signalR.HubConnection;
    constructor() {
        this.connection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Trace)
            .withUrl("/hubs/todo")
            .build();        
    }

    async start() {
        await this.connection.start();
    }

    async getLists() {
        const result = await this.connection.invoke("getLists");
        return result;
    }
}

export const ConnectionServices: PluginObject<any> = {
    install(Vue: VueConstructor<Vue>, option: any | undefined) {
        Vue.$connectionService = new ToDoService();
        Vue.prototype.$connectionService = Vue.$connectionService
    }
}