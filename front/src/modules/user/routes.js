import UserList from './components/UserList'
import UserForm from './components/UserForm'

export default [
  { path: '/user', component: UserList, name: 'user.list' },
  { path: '/user/create', component: UserForm, name: 'user.new' },
  { path: '/user/:id', component: UserForm, name: 'user.edit' }
]
