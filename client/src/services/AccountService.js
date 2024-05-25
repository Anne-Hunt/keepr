import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Keep } from '../models/Keep.js'
import { Vault } from '../models/Vault.js'
import { VaultKeep } from '../models/VaultKeep.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults(){
    const response = await api.get("/account/vaults")
    const vaults = response.data.map(vaultdata => new Vault(vaultdata))
    AppState.vaults = vaults
  }

  async getMyKeeps(){
    const response = await api.get("/account/keeps")
    const keeps = response.data.map(keepdata => new Keep(keepdata))
    AppState.keeps = keeps
  }

  async getMyVaultKeeps(){
    const response = await api.get("/account/vaultkeeps")
    const vaultkeeps = response.data.map(vaultkeep => new VaultKeep(vaultkeep))
    AppState.vaultkeeps = vaultkeeps
  }

  async editAccount(){
    const accountId = AppState.account.id
    const response = await api.put(`/account/${accountId}`)
    const account = new Account(response.data)
    AppState.account = account
  }
}

export const accountService = new AccountService()
