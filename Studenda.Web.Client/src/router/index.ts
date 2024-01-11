import F1 from "../components/F1"
import Admin from "../pages/adminpage"
import Attendance from "../pages/attendancepage"
import Email from "../pages/emailpage"
import GroupSelector from "../pages/groupselectorpage"
import Homework from "../pages/homeworkpage"
import Journal from "../pages/journalpage"
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
    {path: '/journal', element: Journal},
    {path: '/homework', element: Homework},
    {path: '/attendance', element: Attendance},
    {path: '/admin', element: Admin},
    
]