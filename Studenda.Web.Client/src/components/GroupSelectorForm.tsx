import { FC } from 'react'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'

const GroupSelectorForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
              <select style={{margin:'10px'}} name="faculty" id="faculty-select">
                <option value="faculty">--Факультет--</option>
                <option value="fit">ФИТ</option>
                <option value="isf">ИСФ</option>
              </select>  
              <select style={{margin:'10px'}} name="course" id="course-select">
                <option value="0">--Курс--</option>
                <option value="1">1 курс</option>
                <option value="2">2 курс</option>
                <option value="3">3 курс</option>
                <option value="4">4 курс</option>
              </select>   
              <select style={{margin:'10px'}} name="group" id="group-select">
                <option value="0">--Группа--</option>
                <option value="1">Б.ПИН.РИС.21.06</option>
                <option value="2">Б.ПИН.РИС.21.06</option>
                <option value="3">Б.ПИН.РИС.21.06</option>
              </select>    
              <LoginButton variant={ButtonVariant.primary} text='Подтвердить'></LoginButton>           
        </div>

    )
}

export default GroupSelectorForm