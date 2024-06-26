import { reactive } from 'vue'
import { Vault } from './models/Vault.js'
import { Keep } from './models/Keep.js'
import { Profile } from './models/Profile.js'
import { VaultKeep } from './models/VaultKeep.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /**@type {Vault[]}*/
  vaults: [],
  
  /**@type {Keep[]} */
  keeps: [],

  /**@type {VaultKeep[]} */
  vaultkeeps: [],

  /**@type {Vault[]} */
  userVaults: [],
    /**@type {Vault[]} */
    profileVaults: [],

  /**@type {Keep[]} */
  keepsInVault: [],

  /**@type {Profile} */
  activeProfile: null,

  /**@type {Vault} */
  activeVault: null,

  /**@type {Keep} */
  activeKeep: null,

  /**@type {VaultKeep} */
  activeVaultKeep: null
})