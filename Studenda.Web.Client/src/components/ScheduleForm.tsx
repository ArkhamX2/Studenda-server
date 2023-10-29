import { FC } from 'react'
import '../styles/schedule.css';

const ScheduleForm: FC = () => {

    return (
        <main>
                <div className="mainBackground">
                        <div className="buttonBox">
                                <button className="dayButton"> Понедельник </button>
                        </div>
                        <div className="dayBox">
                                <div className="elementBox">
                                        <div className="timeContainer">
                                                <p className="text">10.00</p>
                                                <p className="text">11.00</p>
                                        </div>
                                        <div className="subjectContainer"><p className="text">Дело</p></div>
                                        <div className="roomContainer"><p className="text">каб.123</p></div>
                                </div>
                                <div className="elementBox">
                                        <div className="timeContainer">
                                                <p className="text">10.00</p>
                                                <p className="text">11.00</p>
                                        </div>
                                        <div className="subjectContainer"><p className="text">Дело</p></div>
                                        <div className="roomContainer"><p className="text">каб.123</p></div>
                                </div>
                                <div className="elementBox">
                                        <div className="timeContainer">
                                                <p className="text">10.00</p>
                                                <p className="text">11.00</p>
                                        </div>
                                        <div className="subjectContainer"><p className="text">Дело</p></div>
                                        <div className="roomContainer"><p className="text">каб.123</p></div>
                                </div>
                        </div>
                        <div className="buttonBox">
                                <button className="dayButton"> Вторник </button>
                        </div>
                        <div className="buttonBox">
                                <button className="dayButton"> Среда </button>
                        </div>
                        <div className="buttonBox">
                                <button className="dayButton"> Четверг </button>
                        </div>
                        <div className="buttonBox">
                                <button className="dayButton"> Пятница </button>
                        </div>
                        <div className="buttonBox">
                                <button className="dayButton"> Суббота </button>
                        </div>
        
                </div>
                </main>

    )
}

export default ScheduleForm