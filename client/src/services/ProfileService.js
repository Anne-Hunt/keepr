import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class ProfileService{

    async getProfile(profileId){
        const response = await api.get(`api/profiles/${profileId}`)
        const profile = new Profile(response.data)
        AppState.activeProfile = profile
    }

    async getVaultsByProfile(profileId){
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log(response.data)
        const vaults = response.data.map(vaultData => new Vault(vaultData))
        logger.log(vaults)
        AppState.vaults = vaults
    }
    
    async getKeepsByProfile(profileId){
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        const keeps = response.data.map(keepData => new Keep(keepData))
        AppState.keeps = keeps
    }

}

export const profileService = new ProfileService()