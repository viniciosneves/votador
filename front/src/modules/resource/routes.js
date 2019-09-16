import ResourceList from './components/ResourceList'
import ResourceForm from './components/ResourceForm'

export default [
  { path: '/resource', component: ResourceList, name: 'resource.list' },
  { path: '/resource/create', component: ResourceForm, name: 'resource.new' },
  { path: '/resource/:id', component: ResourceForm, name: 'resource.edit' }
]
