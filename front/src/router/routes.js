import { routes as auth } from '@/modules/auth'
import { routes as user } from '@/modules/user'
import { routes as resource } from '@/modules/resource'
import { routes as vote } from '@/modules/vote'
import Home from '../views/Home.vue'

const baseroute = [
  {
    path: '/',
    name: 'home',
    component: Home
  }
  // {
  //   path: '/about',
  //   name: 'about',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
]

export default [...baseroute, ...auth, ...user, ...resource, ...vote]
