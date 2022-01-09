import { combineReducers } from 'redux'

import accountReducer from './accountReducer'
import exampleReducer from './exampleReducer'
import chatReducer from './chatReducer'


const reducers = combineReducers({
    account: accountReducer,
    chat: chatReducer, 
    example: exampleReducer
})

export default reducers