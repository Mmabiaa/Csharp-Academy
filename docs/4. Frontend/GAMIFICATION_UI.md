# Gamification UI

The frontend translates the backend's XP and achievement data into a rewarding visual experience for the student.

---

## ⚡ XP & Leveling

- **Real-time balance**: Displayed in the `NavBar` via `user.xp` from the `AuthContext`.
- **XP Toasts**: When a lesson is completed, a floating XP toast appears (using the `duo-xp-float` animation).
- **Streak Status**: A fiery streak counter is shown in the header. The UI logic checks the `currentStreak` from the profile.

---

## 🎖️ Badges & Achievements

- **Badge Gallery**: Located on the `Profile.tsx` page. Locked badges stay in grayscale with a tooltip showing how to earn them.
- **Notification Overlays**: When a badge is earned (detected in the `POST /complete` response), the `NotificationContext` displays a full-screen "Congrats!" toast with a specific achievement sound.

---

## 🔊 Sound Integration

The `SoundContext` provides "Auditory Gratification":
- **Success Tone**: Played on lesson pass or practice completion.
- **Fail Tone**: Played on quiz failure or code syntax errors.
- **Coin/XP Sound**: Triggered when XP badges pop up.
- **Click**: Subtle tactile feedback on all `duo-btn` clicks.

---

## 📊 Progress Dashboard

The `ProgressDashboard.tsx` uses interactive cards to show:
- **Daily Activity**: A grid (GitHub style) showingXP-earning days.
- **Leaderboard Position**: Comparison with top platform users.
- **Course Check-ins**: Quick links to resume the most recently touched course.
