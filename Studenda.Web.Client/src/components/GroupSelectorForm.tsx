import { FC } from 'react'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'
import Selector from './UI/selector/Selector'

const GroupSelectorForm: FC = () => {

    return (
        
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
              <h1 style={{alignSelf:'center', fontSize:'20px', marginBottom:'8px'}}>Выбор группы</h1>
              <Selector name="faculty" id='faculty-select' text='Факультет'></Selector>
              <Selector name="course" id='course-select' text='Курс'></Selector>
              <Selector name="group" id='group-select' text='Группа'></Selector>
              <LoginButton variant={ButtonVariant.primary} text='Подтвердить'></LoginButton>           
        </div>

    )
}

export default GroupSelectorForm