import axios from 'axios';

const API_URL ='https://todolist-minimal-api-server.onrender.com';
  //process.env.REACT_APP_API_URL || 'https://todolist-minimal-api-server.onrender.com';

axios.defaults.baseURL = API_URL;
console.log("API URL:", API_URL); // DEBUG

axios.interceptors.response.use(
  response => response,
  error => {
    console.error("API Error:", error);
    return Promise.reject(error);
  }
);

export default {
  getTasks: async () => {
    try {
      const result = await axios.get('/');
      console.log("getTasks result:", result.data); // DEBUG
      return result.data;
    } catch (error) {
      console.error("getTasks error:", error);
      return []; // Return empty array on error
    }
  },

  addTask: async (name, iscomplete) => {
    const result = await axios.post("/", { name, iscomplete });
    return result.data;
  },

  setCompleted: async (id, isComplete) => {
    const result = await axios.put(`/${id}`, { isComplete });
    return result.data;
  },

  deleteTask: async (id) => {
    const result = await axios.delete(`/${id}`);
    return result.data;
  }
};
