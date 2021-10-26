import { combineReducers } from 'redux'

import accountReducer from './accountReducer'
import exampleReducer from './exampleReducer'


const reducers = combineReducers({
    account: accountReducer,
    example: exampleReducer
})

export default reducers