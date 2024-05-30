import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepService{
    unsetActiveKeep() {
        AppState.activeKeep = null
    }
    setActiveKeep(keepId) {
        AppState.activeKeep = null
        const keep = this.getKeepById(keepId)
        const activeKeep = new Keep(keep)
        AppState.activeKeep = activeKeep
    }

    async getKeepsByVault(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        const keeps = response.data.map(keep => new Keep(keep))
        logger.log("keeps", keeps)
        AppState.keepsInVault = keeps
    }

    async getKeeps(){
        const response = await api.get("api/keeps")
        const keeps = response.data.map(keep => new Keep(keep))
        AppState.keeps = keeps
    }

    async updateKeep(keepData, keepId){
        const userId = AppState.account.id
        this.getKeepById(keepId)
        const keepcheck = AppState.activeKeep
        if(userId != keepcheck.creatorId){
            throw new Error("You can't edit what isn't yours!")
        }
        const response = await api.put(`api/keeps/${keepId}`, keepData)
        const keep = new Keep(response.data)
        const updated = AppState.keeps.findIndex(keep => keep.id = keepId)
        AppState.keeps.splice(updated, 1)
        AppState.keeps.push(keep)
    }
    async getKeepById(keepId) {
        const response = await api.get(`api/keeps/${keepId}`)
        const keep = new Keep(response.data)
        AppState.activeKeep = keep
    }

    async createKeep(keepData){
        const response = await api.post("api/keeps", keepData)
        const keep = new Keep(response.data)
        AppState.keeps.push(keep)
    }

    async trashKeep(keepId){
        const userId = AppState.account.id
        this.getKeepById()
        const keepCreator = AppState.activeKeep.creatorId
        if(userId != keepCreator){
            throw new Error("You can't delete what isn't yours!")
        }
        await api.delete(`api/keeps/${keepId}`)
        const trash = AppState.keeps.findIndex(keep => keep.id = keepId)
        AppState.keeps.splice(trash, 1)
        AppState.activeKeep = null
    }
}

export const keepService = new KeepService()