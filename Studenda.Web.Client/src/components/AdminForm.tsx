import { FC } from 'react'
import Selector from './UI/selector/Selector'
import ScheduleTable from './UI/table/ScheduleTable'
import LoginInput, { TextAlign } from './UI/imput/LoginInput'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'

const AdminForm: FC = () => {

    return (
        <main style={{display:'flex', backgroundColor:'white'}}>
            <div style={{display:'flex', flexDirection:'column', border:'2px solid black', margin:'5px', padding:'10px'}}>
                <div style={{display:'flex', flexDirection:'row', margin:'10px 0px 10px 0px'}}>
                    <LoginInput text='Введите группу' align={TextAlign.left}/>
                </div>
              <Selector name="faculty" id='faculty-select' text='Факультет'></Selector>
              <Selector name="course" id='course-select' text='Курс'></Selector>
              <Selector name="group" id='group-select' text='Группа'></Selector>
              <LoginButton text='Допустим отправить' variant={ButtonVariant.primary}></LoginButton>
            </div>
            <div style={{width:'80%', border:'2px solid black', margin:'5px', overflowX:'auto', overflowY:'auto',whiteSpace:'nowrap'}}>
               <ScheduleTable>

               </ScheduleTable>
            </div>
                
        </main>

    )
}

export default AdminForm