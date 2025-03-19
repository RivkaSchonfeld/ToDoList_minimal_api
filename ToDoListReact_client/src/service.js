import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5002';// קנפגתי את הניתוב הכללי 

axios.interceptors.response.use(
  response => response,
  error => {
    console.error("There is some sort of error please notice:", error);
    return Promise.reject(error); //שימשיך למי שקרא לו 
  }
);

export default {
  getTasks: async () => {
    const result = await axios.get('/')
    return result.data;
  },

  addTask: async (name, iscomplete) => {
console.log("trying to add");
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
