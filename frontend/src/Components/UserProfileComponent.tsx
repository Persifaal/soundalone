import { UserProfile } from "../types/UserProfile";
import settingsImage from "../Images/icons/free-icon-settings-126472.png";
import React, { useEffect, useRef, useState } from "react";

type ImageMenuType = "avatar" | "banner" | null;

function UserProfileComponent(props: UserProfile) {
    const [isEditing, setIsEditing] = useState(false);

    const [imageMenu, setImageMenu] = useState<ImageMenuType>(null);
    const [menuPosition, setMenuPosition] = useState({
        x: 0,
        y: 0
    });

    const [displayName, setDisplayName] = useState(props.displayName);
    const [userTag, setUserTag] = useState(props.userTag);
    const [bio, setBio] = useState(props.bio ?? "");

    const avatarRef = useRef<HTMLDivElement>(null);
    const bannerRef = useRef<HTMLDivElement>(null);

    /*
     * Открываем меню именно в месте клика
     */
    const openImageMenu = (
        type: ImageMenuType,
        event: React.MouseEvent
    ) => {
        if (!isEditing) {
            return;
        }

        event.stopPropagation();

        setImageMenu(type);

        setMenuPosition({
            x: event.clientX,
            y: event.clientY
        });
    };

    /*
     * Закрываем меню при клике вне него
     */
    useEffect(() => {
        const handleClickOutside = () => {
            setImageMenu(null);
        };

        if (imageMenu !== null) {
            document.addEventListener("click", handleClickOutside);
        }

        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    }, [imageMenu]);

    const handleMenuClick = (event: React.MouseEvent) => {
        event.stopPropagation();
    };

    const handleRemoveImage = () => {
        if (imageMenu === "avatar") {
            console.log("Удалить аватар");
        }

        if (imageMenu === "banner") {
            console.log("Удалить баннер");
        }

        setImageMenu(null);
    };

    const handleUploadImage = () => {
        if (imageMenu === "avatar") {
            console.log("Загрузить аватар");
        }

        if (imageMenu === "banner") {
            console.log("Загрузить баннер");
        }

        setImageMenu(null);
    };

    const handleSave = () => {
        const updatedProfile = {
            displayName,
            userTag,
            bio
        };

        console.log("Сохраняем:", updatedProfile);

        // Здесь потом:
        // fetch("/api/auth/me", {
        //     method: "PUT",
        //     ...
        // })

        setIsEditing(false);
        setImageMenu(null);
    };

    const handleEdit = () => {
        setIsEditing(prev => !prev);
        setImageMenu(null);
    };
    let bannerElement;

    if (props.bannerUrl !== undefined && props.bannerUrl !== null) {
        bannerElement = (
            <img
                src={props.bannerUrl}
                alt={`${props.displayName} Banner`}
                className="user-banner"
            />
        );
    } else {
        bannerElement = (
            <div className="user-banner"></div>
        );
    }

    return (
        <div className="user-info-container">

            {/* ЛЕВАЯ ПОЛОВИНА */}

            <div className="user-info-block-container">

                {/* БАННЕР */}

                <div
                    ref={bannerRef}
                    className={`user-info-block user-banner-wrapper ${
                        isEditing ? "profile-image-editable" : ""
                    }`}
                    onClick={(event) => openImageMenu("banner", event)}
                >
                    {bannerElement}
                    {!isEditing && (
                        <button
                            type="button"
                            className="user-info-edit-button"
                            onClick={handleEdit}
                        >
                            <img
                                src={settingsImage}
                                alt="Редактировать профиль"
                                className="header-icon inverted"
                            />
                        </button>
                    )}
                    {isEditing && (
                        <div className="profile-image-edit-overlay">
                            <span>Изменить баннер</span>
                        </div>
                    )}

                    {/* МЕНЮ БАННЕРА */}

                    {imageMenu === "banner" && (
                        <div
                            className="profile-image-menu"
                            style={{
                                position: "fixed",
                                left: menuPosition.x,
                                top: menuPosition.y,
                                transform: "none"
                            }}
                            onClick={handleMenuClick}
                        >
                            <button onClick={handleUploadImage}>
                                Загрузить новый
                            </button>

                            {props.bannerUrl && (
                                <button onClick={handleRemoveImage}>
                                    Удалить баннер
                                </button>
                            )}
                        </div>
                    )}
                </div>


                {/* ИНФОРМАЦИЯ */}

                <div className="user-info-block user-info-block-double">

                    {/* TITLE */}

                    <div className="user-info-block-title">

                        {/* АВАТАР */}

                        <div
                            ref={avatarRef}
                            className={`user-info-avatar ${
                                isEditing
                                    ? "profile-image-editable"
                                    : ""
                            }`}
                            onClick={(event) =>
                                openImageMenu("avatar", event)
                            }
                        >
                            <img
                                src={props.avatarUrl}
                                alt={`${props.displayName} avatar`}
                            />

                            {isEditing && (
                                <div className="profile-image-edit-overlay profile-image-edit-overlay-avatar">
                                    <span>Изменить</span>
                                </div>
                            )}

                            {/* МЕНЮ АВАТАРА */}

                            {imageMenu === "avatar" && (
                                <div
                                    className="profile-image-menu"
                                    style={{
                                        position: "fixed",
                                        left: menuPosition.x,
                                        top: menuPosition.y,
                                        transform: "none"
                                    }}
                                    onClick={handleMenuClick}
                                >
                                    <button onClick={handleUploadImage}>
                                        Загрузить новый
                                    </button>

                                    <button onClick={handleRemoveImage}>
                                        Удалить аватар
                                    </button>
                                </div>
                            )}
                        </div>


                        {/* ТЕКСТ ПРОФИЛЯ */}

                        {isEditing ? (
                            <div className="profile-edit-fields">

                                <input
                                    className="profile-edit-field profile-displayname-edit"
                                    value={displayName}
                                    onChange={(event) =>
                                        setDisplayName(event.target.value)
                                    }
                                    placeholder="Имя"
                                />

                                <input
                                    className="profile-edit-field"
                                    value={userTag}
                                    onChange={(event) =>
                                        setUserTag(event.target.value)
                                    }
                                    placeholder="Тег"
                                />

                                <p>Пользователь</p>

                            </div>
                        ) : (
                            <>
                                <h1>{props.displayName}</h1>
                                <h2>{props.userTag}</h2>
                                <p>Пользователь</p>
                            </>
                        )}

                    </div>


                    {/* BIO */}

                    <div className="user-info-block-bio">

                        <p>Обо мне:</p>

                        {isEditing ? (
                            <textarea
                                className="profile-edit-field-bio"
                                value={bio}
                                onChange={(event) =>
                                    setBio(event.target.value)
                                }
                                placeholder="Расскажите о себе"
                            />
                        ) : (
                            <p>{props.bio}</p>
                        )}

                    </div>

                </div>


                {/* ЕЩЁ ОДИН БЛОК */}

                <div className="user-info-block">
                    {/* что-нибудь ещё */}
                </div>


                {/* КНОПКА РЕДАКТИРОВАНИЯ / СОХРАНЕНИЯ */}

                <div className="user-profile-actions">

                    {isEditing ? (
                        <button
                            className="user-profile-save-button"
                            onClick={handleSave}
                        >
                            Сохранить
                        </button>
                    ) : (
                        <button
                            className="user-info-edit-button"
                            onClick={() => setIsEditing(true)}
                        >
                            <img
                                src={settingsImage}
                                alt="settings"
                                className="header-icon inverted"
                            />
                        </button>
                    )}

                </div>

            </div>


            {/* ПРАВАЯ ПОЛОВИНА */}

            <div className="user-info-block-container">

                <div className="user-info-block">
                    {/* содержимое */}
                </div>

                <div className="user-info-block">
                    {/* содержимое */}
                </div>

            </div>

        </div>
    );
}

export default UserProfileComponent;