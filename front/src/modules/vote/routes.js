import ToVoteList from './components/ToVoteList'
import Results from './components/Results'
import Audit from './components/Audit'

export default [
  { path: '/vote', component: ToVoteList, name: 'vote.list' },
  { path: '/vote/result', component: Results, name: 'result.list' },
  { path: '/vote/audit', component: Audit, name: 'audit.list' }
]
