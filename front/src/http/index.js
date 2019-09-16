import axios from 'axios'

const baseAPI = axios.create({
  baseURL: 'http://localhost:56744'
})

baseAPI.interceptors.request.use(
  (config) => {
    let token = localStorage.getItem('token')

    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
    }

    return config
  },

  (error) => {
    return Promise.reject(error)
  }
)

export default baseAPI
