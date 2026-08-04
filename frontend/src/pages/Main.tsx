import React from 'react';
import '../styles/index.css';
import '../styles/App.css';
import '../styles/RotatingCards.css';
// @ts-ignore
import cardImage from '../Images/card.png';
import Header from "../Components/Header";
import Footer from "../Components/Footer";

function Main() {
    // Карточки
    const cards = [
        { id: 1, title: 'Rock', color: '#ff6b6b' },
        { id: 2, title: 'Jazz', color: '#4ecdc4' },
        { id: 3, title: 'Electronic', color: '#45b7d1' },
        { id: 4, title: 'Classical', color: '#f7b731' },
        { id: 5, title: 'Pop', color: '#5f27cd' },
        { id: 6, title: 'Hip-Hop', color: '#ff9ff3' },
        { id: 7, title: 'Blues', color: '#ee5a6f' },
        { id: 8, title: 'Country', color: '#a29bfe' },
        { id: 1, title: 'Rock', color: '#ff6b6b' },
        { id: 2, title: 'Jazz', color: '#4ecdc4' },
        { id: 3, title: 'Electronic', color: '#45b7d1' },
        { id: 4, title: 'Classical', color: '#f7b731' },
        { id: 5, title: 'Pop', color: '#5f27cd' },
        { id: 6, title: 'Hip-Hop', color: '#ff9ff3' },
        { id: 7, title: 'Blues', color: '#ee5a6f' },
        { id: 8, title: 'Country', color: '#a29bfe' },
    ];

  return (
    <div className="App">
        <Header/>
        <main>
            <section className="display-card">
                <div className="container">
                    <div className="orbit-container">
                        <div className="center-card">
                            <img src={cardImage} alt="Center" />
                        </div>

                        <div className="orbit-ring">
                            {cards.map((card, index) => (
                                <div
                                    key={card.id}
                                    className="orbit-card"
                                    style={{
                                        '--card-color': card.color,
                                        '--rotation': `${(360 / cards.length) * index}deg`,
                                    } as React.CSSProperties}
                                >
                                    <div className="orbit-card-face">

                                        <div className="orbit-card-content orbit-card-front">
                                            <h3>{card.title}</h3>
                                        </div>

                                        <div className="orbit-card-content orbit-card-back">
                                            <h3>{card.title}</h3>
                                        </div>

                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="card-wrapper">
                        <div className="card">
                            <div className="card-header">
                                <h1>Присоединяйтесь к активному сообществу SoundAlone</h1>
                            </div>
                            <div className="card-body">
                                <p>Наше живое коммьюнити делится тысячами треков друг с другом каждый день!
                                    <br></br><br></br>
                                    Номинируйте музыкантов, плейлисты и треки. Слушайте музыку вместе с друзьями где угодно и когда угодно без ограничений</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </main>
        <Footer/>
    </div>
  );
}

export default Main;