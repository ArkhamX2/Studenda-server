import F1 from "../components/F1"
import Email from "../pages/emailpage"
import GroupSelector from "../pages/groupselectorpage"
import Login from "../pages/loginpage"
import Schedule from "../pages/schedulepage"

export const privateRoutes = [

]

export const publicRoutes = [
    {path: '/login', element: Login},
    {path: '/email', element: Email},
    {path: '/f1', element: F1},
    {path: '/groupselector', element: GroupSelector},
    {path: '/schedule', element: Schedule},
]